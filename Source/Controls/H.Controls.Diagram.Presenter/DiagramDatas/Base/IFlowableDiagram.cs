using H.Controls.Diagram.Flowables;

namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public interface IFlowableDiagramData : IDiagramData, IMessageable
{
    DiagramFlowableMode FlowableMode { get; set; }
    DiagramFlowableState State { get; set; }
    Task<bool?> Start();
    //Task<bool?> InvokeNode(Node startNode);
}
