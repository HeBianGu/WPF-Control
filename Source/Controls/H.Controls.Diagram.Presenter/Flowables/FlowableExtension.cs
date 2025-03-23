// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Mvvm;
using H.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace H.Controls.Diagram.Presenter.Flowables;

public static class FlowableExtension
{
    //public static Node GetStartNode(this IEnumerable<Node> nodes, out string message)
    //{
    //    message = null;
    //    var parts = nodes.SelectMany(x => x.GetParts()).OfType<FlowablePart>();
    //    foreach (FlowablePart part in parts)
    //    {
    //        //part.State = FlowableState.Wait;
    //        if (part.Content is IFlowablePartData flowable)
    //            flowable.State = FlowableState.Wait;
    //    }
    //    IEnumerable<Node> starts = nodes.Where(x => x.GetFromNodes().Count == 0);

    //    if (starts == null || starts.Count() == 0)
    //    {
    //        message = "未找到起始节点,请添加UseStart节点";
    //        return null;
    //    }

    //    if (starts.Count() > 1)
    //    {
    //        message = "查询到多个节点";
    //        return null;
    //    }
    //    return starts.FirstOrDefault();
    //}

    //public static async Task<bool?> Start(this Node startNode, DiagramFlowableMode flowableMode, Action<Part> invoking = null, Action<Part> invoked = null)
    //{
    //    if (flowableMode == DiagramFlowableMode.Link)
    //        return await startNode.InvokeLink(invoking, invoked);
    //    else if (flowableMode == DiagramFlowableMode.Port)
    //        return await startNode.InvokePort(invoking, invoked);
    //    else
    //        return await startNode.InvokeNode(invoking, invoked);
    //}

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
        return state != DiagramFlowableState.Canceling;
    }
    public static bool CanClear(this DiagramFlowableState state)
    {
        return state != DiagramFlowableState.Running && state != DiagramFlowableState.Canceling;
    }

    public static void Stop(this IEnumerable<Node> nodes)
    {
        nodes.GotoState(x =>
        {
            if (x.State == FlowableState.Running || x.State == FlowableState.Wait || x.State == FlowableState.Ready)
                return FlowableState.Canceling;
            return null;
        });
    }

    public static void Reset(this IEnumerable<Node> nodes)
    {
        nodes.GotoState(x => FlowableState.Ready);
    }

    public static void Wait(this IEnumerable<Node> nodes)
    {
        nodes.GotoState(x => FlowableState.Wait);
    }

    public static void GotoState(this IEnumerable<Node> nodes, Func<IFlowablePartData, FlowableState?> gotoState)
    {
        var parts = nodes.SelectMany(x => x.GetParts()).OfType<FlowablePart>();
        foreach (var part in parts)
        {
            if (part.Content is IFlowablePartData flowable)
            {
                var state = gotoState(flowable);
                if (state == null)
                    continue;
                flowable.State = state.Value;
            }
        }
    }

    //public static async Task<bool?> InvokePort(this Node node, Action<Part> invoking = null, Action<Part> invoked = null)
    //{
    //    var diagramData = node.GetDiagram().DataContext as IFlowableDiagramData;
    //    Func<Node, Port, Task<bool?>> run = null;
    //    run = async (cNode, from) =>
    //    {
           
    //        if (cNode.GetContent<IFlowableNodeData>() is IFlowableNodeData nodeData)
    //        {
    //            if (nodeData.State == FlowableState.Canceling)
    //                return null;
    //            //this.GetDiagram().OnRunningPartChanged(l);
    //            FlowableResult result;
    //            using (new PartInvokable(cNode, invoking, invoked))
    //            {
    //                result = await nodeData.TryInvokeAsync(from.GetContent<IFlowablePartData>(), diagramData) as FlowableResult;
    //                invoked?.Invoke(cNode);
    //                if (result == null || result.State == FlowableResultState.Error)
    //                    return false;
    //            }

    //            IEnumerable<Link> links = cNode.LinksOutOf.Where(x => x.GetContent<IFlowableLinkData>().IsMatchResult(result));
    //            foreach (Link link in links)
    //            {
    //                IFlowableLinkData linkData = link.GetContent<IFlowableLinkData>();
    //                if (linkData.State == FlowableState.Canceling)
    //                    return null;
    //                invoking?.Invoke(link.FromPort);
    //                IFlowablePortData portData = link.FromPort?.GetContent<IFlowablePortData>();
    //                //this.GetDiagram().OnRunningPartChanged(link.FromPort);
    //                using (new PartInvokable(link.FromPort, invoking, invoked))
    //                {
    //                    IFlowableResult rFrom = await portData?.TryInvokeAsync(cNode.GetContent<IFlowablePartData>(), diagramData);
    //                    if (rFrom?.State == FlowableResultState.Error)
    //                        return false;
    //                }

    //                if (linkData.State == FlowableState.Canceling)
    //                    return null;
    //                //link.InvokeDispatcher(x => x.State = FlowableState.Running);
    //                ////this.GetDiagram().OnRunningPartChanged(link);
    //                linkData.State = FlowableState.Running;
    //                using (new PartInvokable(link, invoking, invoked))
    //                {
    //                    IFlowableResult r = await linkData?.TryInvokeAsync(link.FromPort.GetContent<IFlowablePartData>(), diagramData);
    //                    linkData.State = r?.State == FlowableResultState.OK ? FlowableState.Success : FlowableState.Error;
    //                    if (r?.State == FlowableResultState.Error)
    //                        return false;
    //                }
    //                if (nodeData.State == FlowableState.Canceling)
    //                    return null;
    //                using (new PartInvokable(link.ToPort, invoking, invoked))
    //                {
    //                    IFlowablePortData toPort = link.ToPort.GetContent<IFlowablePortData>();
    //                    //this.GetDiagram().OnRunningPartChanged(link.ToPort);
    //                    IFlowableResult rTo = await toPort?.TryInvokeAsync(link.GetContent<IFlowablePartData>(), diagramData);
    //                    invoked?.Invoke(link.ToPort);
    //                    if (rTo?.State == FlowableResultState.Error)
    //                        return false;
    //                }

    //                //  Do ：递归执行ToNode
    //                bool? b = await run?.Invoke(link.ToNode, link.ToPort);
    //                if (b != true) return b;
    //            }
    //        }
    //        return true;
    //    };
    //    return await run.Invoke(node, null);
    //}

    //public static async Task<bool?> InvokeLink(this Node node, Action<Part> invoking = null, Action<Part> invoked = null)
    //{
    //    var diagramData = node.GetDiagram().DataContext as IFlowableDiagramData;

    //    Func<Node, Link, Task<bool?>> run = null;
    //    run = async (cNode, from) =>
    //    {
    //        if (cNode.Data is IFlowableNodeData nodeData)
    //        {
    //            if (nodeData.State == FlowableState.Canceling)
    //                return null;
    //            using (new PartInvokable(cNode, invoking, invoked))
    //            {
    //                FlowableResult result = await nodeData.TryInvokeAsync(from.GetContent<IFlowablePartData>(), diagramData) as FlowableResult;
    //                if (result == null || result.State == FlowableResultState.Error)
    //                    return false;
    //            }
    //            foreach (Link link in cNode.LinksOutOf)
    //            {
    //                if (nodeData.State == FlowableState.Canceling)
    //                    return null;
    //                IFlowableLinkData linkData = link.GetContent<IFlowableLinkData>();
    //                linkData.State = FlowableState.Running;
    //                using (new PartInvokable(link, invoking, invoked))
    //                {
    //                    IFlowableResult r = await linkData?.TryInvokeAsync(cNode.GetContent<IFlowablePartData>(), diagramData);
    //                    linkData.State = r?.State == FlowableResultState.OK ? FlowableState.Success : FlowableState.Error;
    //                    if (r?.State == FlowableResultState.Error)
    //                        return false;
    //                }
    //                bool? b = await run?.Invoke(link.ToNode, link);
    //                if (b != true) return b;
    //            }
    //        }
    //        return true;
    //    };

    //    return await run.Invoke(node, null);
    //}

    //public static async Task<bool?> InvokeNode(this Node startNode, DiagramFlowableMode flowableMode, Action<Part> invoking = null, Action<Part> invoked = null)
    //{
    //    return flowableMode == DiagramFlowableMode.Link
    //        ? await startNode.InvokeLink(invoking, invoked)
    //        : flowableMode == DiagramFlowableMode.Port ? await startNode.InvokePort(invoking, invoked) : await startNode.InvokeNode(invoking, invoked);
    //}

    //public static async Task<bool?> InvokeNode(this Node node, Action<Part> invoking = null, Action<Part> invoked = null)
    //{
    //    var diagramData = node.GetDiagram().DataContext as IFlowableDiagramData;

    //    Func<Node, Task<bool?>> run = null;
    //    run = async l =>
    //    {
    //        List<Node> tos = l.GetToNodes();
    //        foreach (Node node in tos)
    //        {
    //            if (node.Data is IFlowableNodeData data)
    //            {
    //                if (data.State == FlowableState.Canceling)
    //                    return null;
    //                using (new PartInvokable(node, invoking, invoked))
    //                {
    //                    IFlowableResult result = await data.TryInvokeAsync(l.GetContent<IFlowablePartData>(), diagramData);
    //                    if (result.State == FlowableResultState.Error)
    //                        return false;
    //                }
    //                bool? b = await run.Invoke(node);
    //                if (b != true)
    //                    return b;
    //            }
    //        }
    //        return true;
    //    };

    //    using (new PartInvokable(node, invoking, invoked))
    //    {
    //        var r = await node.GetContent<IFlowableNodeData>().TryInvokeAsync(null, diagramData);
    //        if (r.State == FlowableResultState.Error)
    //            return false;
    //    }
    //    return await run.Invoke(node);

    //}

    public static FlowableState ToState(this IFlowableResult? result)
    {
        if (result == null)
            return FlowableState.Canceling;
        return result.State == FlowableResultState.OK ? FlowableState.Success : FlowableState.Error;
    }


    public static DiagramFlowableState ToDiagramFlowableState(this bool? value)
    {
        if (value == null)
            return DiagramFlowableState.Canceled;
        return value == true ? DiagramFlowableState.Success : DiagramFlowableState.Error;
    }
    public static FlowableState ToFlowableState(this bool? value)
    {
        if (value == null)
            return FlowableState.Canceled;
        return value == true ? FlowableState.Success : FlowableState.Error;
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

public class PartDataInvokable : IDisposable
{
    private Action<IPartData> _invoked;
    private IPartData _part;

    public PartDataInvokable(IPartData part, Action<IPartData> invoking, Action<IPartData> invoked)
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
