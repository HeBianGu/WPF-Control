namespace H.Controls.Diagram.Presenter.DiagramDatas;

public interface IFlowableDiagramData : IDiagramData
{
    DiagramFlowableMode StartMode { get; set; }
    DiagramFlowableState State { get; set; }
    string Message { get; set; }
    Task<bool?> Start();
    Task<bool?> Start(Node startNode);
}
