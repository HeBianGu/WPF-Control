
namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public interface IFlowableDiagramData : IDiagramData, IMessageable, IPartDataInvokeable
{
    [Obsolete("始终执行所有Part")]
    DiagramFlowableMode FlowableMode { get; set; }
    DiagramFlowableState State { get; set; }
    Task<bool?> Start();
}

public static class FlowableDiagramDataExtension
{
    public static async Task<bool?> InvokeState(this IFlowableDiagramData data, Func<Task<bool?>> action)
    {
        data.State = DiagramFlowableState.Running;
        data.Message = "正在运行";
        data.Wait();
        var b = await action?.Invoke();
        data.State = b.ToDiagramFlowableState();
        var message = b == null ? "用户取消" : b == true ? "运行成功" : "运行失败";
        IocMessage.Snack?.ShowInfo(message);
        H.Mvvm.Commands.InvalidateRequerySuggested();
        data.Message = message;
        return b;
    }

    //[Obsolete("NodeData>InvokeCurrent")]
    //public static async Task<bool?> Invoke(this IFlowableNodeData startNode, IFlowableDiagramData diagramData)
    //{
    //    return diagramData.FlowableMode == DiagramFlowableMode.Link
    //        ? await startNode.InvokeLink(diagramData)
    //        : diagramData.FlowableMode == DiagramFlowableMode.Port ? await startNode.InvokePort(diagramData) : await startNode.InvokeNode(diagramData);
    //}

    //[Obsolete("NodeData>InvokeCurrent")]
    //public static async Task<bool?> InvokeNode(this IFlowableNodeData node, IFlowableDiagramData diagramData)
    //{
    //    Func<IFlowableNodeData, Task<bool?>> run = null;
    //    run = async l =>
    //    {
    //        var tos = l.GetToNodeDatas(diagramData).OfType<IFlowableNodeData>();
    //        foreach (IFlowableNodeData data in tos)
    //        {
    //            if (data.State == FlowableState.Canceling)
    //                return null;
    //            using (new PartDataInvokable(data, diagramData.OnInvokingPart, diagramData.OnInvokedPart))
    //            {
    //                IFlowableResult result = await data.TryInvokeAsync(null,diagramData);
    //                if (result.State == FlowableResultState.Error)
    //                    return false;
    //            }
    //            bool? b = await run.Invoke(data);
    //            if (b != true)
    //                return b;
    //        }
    //        return true;
    //    };

    //    using (new PartDataInvokable(node, diagramData.OnInvokingPart, diagramData.OnInvokedPart))
    //    {
    //        var r = await node.TryInvokeAsync(null, diagramData);
    //        if (r.State == FlowableResultState.Error)
    //            return false;
    //    }
    //    return await run.Invoke(node);

    //}
    //[Obsolete("NodeData>InvokeCurrent")]
    //public static async Task<bool?> InvokeLink(this IFlowableNodeData node, IFlowableDiagramData diagramData)
    //{
    //    Func<IFlowableNodeData, IFlowableLinkData, Task<bool?>> run = null;
    //    run = async (nodeData, from) =>
    //    {
    //        if (nodeData.State == FlowableState.Canceling)
    //            return null;
    //        using (new PartDataInvokable(nodeData, diagramData.OnInvokingPart, diagramData.OnInvokedPart))
    //        {
    //            FlowableResult result = await nodeData.TryInvokeAsync(from, diagramData) as FlowableResult;
    //            if (result == null || result.State == FlowableResultState.Error)
    //                return false;
    //        }

    //        var toLinks = nodeData.GetToLinkDatas(diagramData).OfType<IFlowableLinkData>();
    //        foreach (var linkData in toLinks)
    //        {
    //            if (nodeData.State == FlowableState.Canceling)
    //                return null;
    //            linkData.State = FlowableState.Running;
    //            using (new PartDataInvokable(linkData, diagramData.OnInvokingPart, diagramData.OnInvokedPart))
    //            {
    //                IFlowableResult r = await linkData?.TryInvokeAsync(nodeData, diagramData);
    //                linkData.State = r?.State == FlowableResultState.OK ? FlowableState.Success : FlowableState.Error;
    //                if (r?.State == FlowableResultState.Error)
    //                    return false;
    //            }
    //            var toNodeData = linkData.GetToNodeData(diagramData);
    //            if (toNodeData is IFlowableNodeData flowableNodeData)
    //            {
    //                bool? b = await run?.Invoke(flowableNodeData, linkData);
    //                if (b != true) return b;
    //            }
    //        }
    //        return true;
    //    };

    //    return await run.Invoke(node, null);
    //}
    //[Obsolete("NodeData>InvokeCurrent")]
    //public static async Task<bool?> InvokePort(this IFlowableNodeData node, IFlowableDiagramData diagramData)
    //{
    //    Func<IFlowableNodeData, IFlowablePortData, Task<bool?>> run = null;
    //    run = async (nodeData, from) =>
    //    {
    //        if (nodeData.State == FlowableState.Canceling)
    //            return null;
    //        //this.GetDiagram().OnRunningPartChanged(l);
    //        FlowableResult result;
    //        using (new PartDataInvokable(nodeData, diagramData.OnInvokingPart, diagramData.OnInvokedPart))
    //        {
    //            result = await nodeData.TryInvokeAsync(from, diagramData) as FlowableResult;
    //            if (result == null || result.State == FlowableResultState.Error)
    //                return false;
    //        }

    //        var toLinks = nodeData.GetToLinkDatas(diagramData).OfType<IFlowableLinkData>().Where(x => x.IsMatchResult(result));

    //        //IEnumerable<Link> links = cNode.LinksOutOf.Where(x => x.GetContent<IFlowableLinkData>().IsMatchResult(result));
    //        foreach (var linkData in toLinks)
    //        {
    //            if (linkData.State == FlowableState.Canceling)
    //                return null;
    //            IFlowablePortData fPort = linkData.GetFromPortData(diagramData) as IFlowablePortData;
    //            //IFlowablePortData portData = link.FromPort?.GetContent<IFlowablePortData>();
    //            //this.GetDiagram().OnRunningPartChanged(link.FromPort);
    //            using (new PartDataInvokable(fPort, diagramData.OnInvokingPart, diagramData.OnInvokedPart))
    //            {
    //                IFlowableResult rFrom = await fPort?.TryInvokeAsync(nodeData, diagramData);
    //                if (rFrom?.State == FlowableResultState.Error)
    //                    return false;
    //            }

    //            if (linkData.State == FlowableState.Canceling)
    //                return null;
    //            //link.InvokeDispatcher(x => x.State = FlowableState.Running);
    //            ////this.GetDiagram().OnRunningPartChanged(link);
    //            linkData.State = FlowableState.Running;
    //            using (new PartDataInvokable(linkData, diagramData.OnInvokingPart, diagramData.OnInvokedPart))
    //            {
    //                IFlowableResult r = await linkData?.TryInvokeAsync(fPort, diagramData);
    //                linkData.State = r?.State == FlowableResultState.OK ? FlowableState.Success : FlowableState.Error;
    //                if (r?.State == FlowableResultState.Error)
    //                    return false;
    //            }
    //            if (nodeData.State == FlowableState.Canceling)
    //                return null;
    //            IFlowablePortData tPort = linkData.GetFromPortData(diagramData) as IFlowablePortData;
    //            using (new PartDataInvokable(tPort, diagramData.OnInvokingPart, diagramData.OnInvokedPart))
    //            {
    //                //IFlowablePortData toPort = link.ToPort.GetContent<IFlowablePortData>();
    //                //this.GetDiagram().OnRunningPartChanged(link.ToPort);
    //                IFlowableResult rTo = await tPort?.TryInvokeAsync(linkData, diagramData);
    //                if (rTo?.State == FlowableResultState.Error)
    //                    return false;
    //            }

    //            var tNodeData = linkData.GetToNodeData(diagramData) as IFlowableNodeData;
    //            //  Do ：递归执行ToNode
    //            bool? b = await run?.Invoke(tNodeData, tPort);
    //            if (b != true) return b;
    //        }
    //        return true;
    //    };
    //    return await run.Invoke(node, null);
    //}



    public static void Reset(this IFlowableDiagramData flowableDiagramData)
    {
        flowableDiagramData.GotoState(x => FlowableState.Ready);
    }

    public static void Wait(this IFlowableDiagramData flowableDiagramData)
    {
        flowableDiagramData.GotoState(x => FlowableState.Wait);
    }

    public static IEnumerable<IPartData> GetPartDatas(this INodeData nodeData, IDiagramData diagramData, Func<IPartData, bool> filter = null)
    {
        yield return nodeData;
        var ports = nodeData.GetPortDatas(diagramData);

        foreach (IPartData item in ports)
        {
            if (filter?.Invoke(item) != false)
                yield return item;
        }
        var links = nodeData.GetToLinkDatas(diagramData);
        foreach (IPartData item in links)
        {
            if (filter?.Invoke(item) != false)
                yield return item;
        }
    }

    public static void GotoState(this IFlowableDiagramData flowableDiagramData, Func<IFlowablePartData, FlowableState?> gotoState)
    {
        flowableDiagramData.NodeDatas.OfType<IFlowableNodeData>().GotoState(flowableDiagramData, gotoState);
    }

    public static void GotoState(this IEnumerable<IFlowableNodeData> nodeDatas, IFlowableDiagramData flowableDiagramData, Func<IFlowablePartData, FlowableState?> gotoState)
    {
        var parts = nodeDatas.SelectMany(x => x.GetPartDatas(flowableDiagramData)).OfType<IFlowablePartData>();
        parts.GotoState(gotoState);
    }

    public static void GotoState(this IEnumerable<IFlowablePartData> partDatas, Func<IFlowablePartData, FlowableState?> gotoState)
    {
        foreach (var part in partDatas)
        {
            var state = gotoState(part);
            if (state == null)
                continue;
            part.State = state.Value;
        }
    }
}
