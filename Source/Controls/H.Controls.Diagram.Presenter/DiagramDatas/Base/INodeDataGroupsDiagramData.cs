namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public interface INodeDataGroupsDiagramData : IDiagramData
{
    ObservableCollection<INodeDataGroup> NodeGroups { get; set; }
}
