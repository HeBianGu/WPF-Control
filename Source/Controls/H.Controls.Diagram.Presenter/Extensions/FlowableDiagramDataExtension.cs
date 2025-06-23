// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.Extensions;

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
        IocMessage.ShowSnackInfo(message);
        H.Extensions.Mvvm.Commands.Commands.InvalidateRequerySuggested();
        data.Message = message;
        return b;
    }

    public static void Reset(this IFlowableDiagramData flowableDiagramData)
    {
        flowableDiagramData.GotoState(x => FlowableState.Ready);
    }

    public static void Wait(this IFlowableDiagramData flowableDiagramData, Func<IFlowableNodeData, bool> match = null)
    {
        flowableDiagramData.GotoState(x => FlowableState.Wait, match);
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

    public static void GotoState(this IFlowableDiagramData flowableDiagramData, Func<IFlowablePartData, FlowableState?> gotoState, Func<IFlowableNodeData, bool> match = null)
    {
        flowableDiagramData.NodeDatas.OfType<IFlowableNodeData>().Where(x => match?.Invoke(x) != false).GotoState(flowableDiagramData, gotoState);
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
