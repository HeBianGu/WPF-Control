using H.Controls.Diagram.Flowables;
using H.Mvvm.ViewModels.Base;

namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public interface IFlowableDiagramData : IDiagramData, IMessageable
{
    DiagramFlowableMode FlowableMode { get; set; }
    DiagramFlowableState State { get; set; }
    Task<bool?> Start();
    //Task<bool?> InvokeNode(Node startNode);
}
