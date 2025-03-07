using H.Controls.Diagram.Flowables;
using H.Mvvm.ViewModels.Base;

namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public interface IFlowableDiagramData : IDiagramData, IMessageable
{
    DiagramFlowableMode FlowableMode { get; set; }
    DiagramFlowableState State { get; set; }
    Task<bool?> Start();
    //Task<bool?> InvokeNode(Node startNode);
    void Wait();
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
}
