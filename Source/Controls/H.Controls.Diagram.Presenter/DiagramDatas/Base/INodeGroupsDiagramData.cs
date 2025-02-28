namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public interface INodeGroupsDiagramData : IDiagramData
{
    ObservableCollection<INodeDataGroup> NodeGroups { get; set; }
}
