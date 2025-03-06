// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace H.Controls.Diagram.Flowables;

public static class FlowableExtension
{
    public static Node GetStartNode(this IEnumerable<Node> nodes, out string message)
    {
        message = null;
        var parts = nodes.SelectMany(x => x.GetParts()).OfType<FlowablePart>();
        foreach (FlowablePart part in parts)
        {
            part.State = FlowableState.Wait;
            if (part.Content is IFlowable flowable)
                flowable.State = FlowableState.Wait;
        }
        IEnumerable<Node> starts = nodes.Where(x => x.GetFromNodes().Count == 0 && x.GetContent<IFlowableNodeData>()?.UseStart == true);

        if (starts == null || starts.Count() == 0)
        {
            message = "未找到起始节点,请添加UseStart节点";
            return null;
        }

        if (starts.Count() > 1)
        {
            message = "查询到多个节点";
            return null;
        }
        return starts.FirstOrDefault();
    }

    //public static async Task<string> Start(this IEnumerable<Node> nodes, DiagramFlowableMode flowableMode, Action<DiagramFlowableState> stateAction, Action<Part> invoking = null, Action<Part> invoked = null)
    //{
    //    Node node = nodes.GetStartNode(out string message);
    //    if (node == null)
    //        return message;
    //    stateAction?.Invoke(DiagramFlowableState.Running);
    //    bool? b = await node.Start(flowableMode, invoking, invoked);
    //    DiagramFlowableState state = b == null ? DiagramFlowableState.Canceled : b == true ? DiagramFlowableState.Success : DiagramFlowableState.Error;
    //    stateAction?.Invoke(state);
    //    Commands.InvalidateRequerySuggested();
    //    if (b == null)
    //        return "用户取消";
    //    return null;
    //    //return b == null ? "用户取消" : b == true ? "运行成功" : "运行失败";
    //}

    public static async Task<bool?> Start(this Node startNode, DiagramFlowableMode flowableMode, Action<Part> invoking = null, Action<Part> invoked = null)
    {
        if (flowableMode == DiagramFlowableMode.Link)
            return await startNode.InvokeLink(invoking, invoked);
        else if (flowableMode == DiagramFlowableMode.Port)
            return await startNode.InvokePort(invoking, invoked);
        else
            return await startNode.InvokeNode(invoking, invoked);
    }

    public static bool CanStart(this DiagramFlowableState state)
    {
        return state != DiagramFlowableState.Running && state != DiagramFlowableState.Canceling;
    }
    public static bool CanStop(this DiagramFlowableState state)
    {
        return state == DiagramFlowableState.Running && state != DiagramFlowableState.Canceling;
    }

    public static bool CanReset(this DiagramFlowableState state)
    {
        return state != DiagramFlowableState.None && state != DiagramFlowableState.Canceling;
    }
    public static bool CanClear(this DiagramFlowableState state)
    {
        return state != DiagramFlowableState.Running && state != DiagramFlowableState.Canceling;
    }

    public static void Stop(this IEnumerable<Node> nodes)
    {
        IEnumerable<FlowablePart> parts = nodes.SelectMany(x => x.GetParts()).OfType<FlowablePart>();
        foreach (FlowablePart part in parts)
        {
            IFlowable data = part.GetContent<IFlowable>();
            if (data != null)
            {
                if (data.State == FlowableState.Running || data.State == FlowableState.Wait)
                    data.State = FlowableState.Canceling;
            }
            if (part.State == FlowableState.Running || part.State == FlowableState.Wait)
                part.State = FlowableState.Canceling;
        }
    }

    public static void Reset(this IEnumerable<Node> nodes)
    {
        var parts = nodes.SelectMany(x => x.GetParts()).OfType<FlowablePart>();
        foreach (var part in parts)
        {
            part.State = FlowableState.Ready;
            if (part.Content is IFlowable flowable)
                flowable.State = FlowableState.Ready;
        }
    }

    public static async Task<bool?> InvokePort(this Node node, Action<Part> invoking = null, Action<Part> invoked = null)
    {
        Func<Node, Port, Task<bool?>> run = null;
        run = async (cNode, from) =>
        {
            if (cNode.GetContent<IFlowableNodeData>() is IFlowableNodeData nodeData)
            {
                if (node.GetDispatcherValue(x => x.State) == FlowableState.Canceling)
                    return null;
                //this.GetDiagram().OnRunningPartChanged(l);
                FlowableResult result;
                using (new PartInvokable(cNode, invoking, invoked))
                {
                    result = await nodeData.TryInvokeAsync(from, cNode) as FlowableResult;
                    invoked?.Invoke(cNode);
                    if (result == null || result.State == FlowableResultState.Error)
                        return false;
                }

                IEnumerable<Link> links = cNode.LinksOutOf.Where(x => x.GetContent<IFlowableLink>().IsMatchResult(result));
                foreach (Link link in links)
                {
                    if (link.GetDispatcherValue(x => x.State) == FlowableState.Canceling)
                        return null;
                    invoking?.Invoke(link.FromPort);
                    IFlowablePort portData = link.FromPort?.GetContent<IFlowablePort>();
                    //this.GetDiagram().OnRunningPartChanged(link.FromPort);
                    using (new PartInvokable(link.FromPort, invoking, invoked))
                    {
                        IFlowableResult rFrom = await portData?.TryInvokeAsync(cNode, link.FromPort);
                        if (rFrom?.State == FlowableResultState.Error)
                            return false;
                    }

                    if (link.GetDispatcherValue(x => x.State) == FlowableState.Canceling)
                        return null;
                    IFlowableLink linkData = link.GetContent<IFlowableLink>();
                    link.InvokeDispatcher(x => x.State = FlowableState.Running);
                    //this.GetDiagram().OnRunningPartChanged(link);
                    using (new PartInvokable(link, invoking, invoked))
                    {
                        IFlowableResult r = await linkData?.TryInvokeAsync(link.FromPort, link);
                        link.InvokeDispatcher(x => link.State = r?.State == FlowableResultState.OK ? FlowableState.Success : FlowableState.Error);
                        if (r?.State == FlowableResultState.Error)
                            return false;
                    }
                    if (node.GetDispatcherValue(x => x.State) == FlowableState.Canceling)
                        return null;
                    using (new PartInvokable(link.ToPort, invoking, invoked))
                    {
                        IFlowablePort toPort = link.ToPort.GetContent<IFlowablePort>();
                        //this.GetDiagram().OnRunningPartChanged(link.ToPort);
                        IFlowableResult rTo = await toPort?.TryInvokeAsync(link, link.ToPort);
                        invoked?.Invoke(link.ToPort);
                        if (rTo?.State == FlowableResultState.Error)
                            return false;
                    }

                    //  Do ：递归执行ToNode
                    bool? b = await run?.Invoke(link.ToNode, link.ToPort);
                    if (b != true) return b;
                }
            }
            return true;
        };
        return await run.Invoke(node, null);
    }

    public static async Task<bool?> InvokeLink(this Node node, Action<Part> invoking = null, Action<Part> invoked = null)
    {
        Func<Node, Link, Task<bool?>> run = null;
        run = async (cNode, from) =>
        {
            if (cNode.Content is IFlowableNodeData nodeData)
            {
                if (node.State == FlowableState.Canceling)
                    return null;
                using (new PartInvokable(cNode, invoking, invoked))
                {
                    FlowableResult result = await nodeData.TryInvokeAsync(from, cNode) as FlowableResult;
                    if (result == null || result.State == FlowableResultState.Error)
                        return false;
                }
                foreach (Link link in cNode.LinksOutOf)
                {
                    if (node.State == FlowableState.Canceling)
                        return null;
                    IFlowableLink linkData = link.GetContent<IFlowableLink>();
                    link.State = FlowableState.Running;
                    using (new PartInvokable(link, invoking, invoked))
                    {
                        IFlowableResult r = await linkData?.TryInvokeAsync(cNode, link);
                        link.State = r?.State == FlowableResultState.OK ? FlowableState.Success : FlowableState.Error;
                        if (r?.State == FlowableResultState.Error)
                            return false;
                    }
                    bool? b = await run?.Invoke(link.ToNode, link);
                    if (b != true) return b;
                }
            }
            return true;
        };

        return await run.Invoke(node, null);
    }

    public static async Task<bool?> InvokeNode(this Node startNode, DiagramFlowableMode flowableMode, Action<Part> invoking = null, Action<Part> invoked = null)
    {
        return flowableMode == DiagramFlowableMode.Link
            ? await startNode.InvokeLink(invoking, invoked)
            : flowableMode == DiagramFlowableMode.Port ? await startNode.InvokePort(invoking, invoked) : await startNode.InvokeNode(invoking, invoked);
    }

    public static async Task<bool?> InvokeNode(this Node node, Action<Part> invoking = null, Action<Part> invoked = null)
    {
        Func<Node, Task<bool?>> run = null;
        run = async l =>
        {
            List<Node> tos = l.GetToNodes();
            foreach (Node node in tos)
            {
                if (node.GetDispatcherValue(x => x.State) == FlowableState.Canceling)
                    return null;
                if (node.GetContent<IFlowableNodeData>() is IFlowableNodeData data)
                {
                    using (new PartInvokable(node, invoking, invoked))
                    {
                        IFlowableResult result = await data.TryInvokeAsync(l, node);
                        if (result.State == FlowableResultState.Error)
                            return false;
                    }
                    bool? b = await run.Invoke(node);
                    if (b != true)
                        return b;
                }
            }
            return true;
        };

        using (new PartInvokable(node, invoking, invoked))
        {
            await node.GetContent<IFlowableNodeData>().TryInvokeAsync(null, node);
        }
        return await run.Invoke(node);

    }
}

public class PartInvokable : IDisposable
{
    private Action<Part> _invoked;
    private Part _part;

    public PartInvokable(Part part, Action<Part> invoking, Action<Part> invoked)
    {
        this._part = part;
        invoking?.Invoke(part);
        _invoked = invoked;
    }

    public void Dispose()
    {
        _invoked?.Invoke(this._part);
    }
}
