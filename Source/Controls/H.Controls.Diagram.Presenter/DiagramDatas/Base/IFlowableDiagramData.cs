global using H.Controls.Diagram.Presenter.Flowables;
using H.Common.Interfaces;
namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public interface IFlowableDiagramData : IDiagramData, IMessageable, IPartDataInvokeable
{
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
        IocMessage.ShowSnackInfo(message);
        H.Mvvm.Commands.Commands.InvalidateRequerySuggested();
        data.Message = message;
        return b;
    }

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
