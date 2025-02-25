namespace H.Controls.Diagram.Presenter.DiagramDatas;

public interface IDiagramData : ICloneable
{
    string ID { get; set; }
    string Name { get; set; }
    string GroupName { get; set; }
    string TypeName { get; set; }
    //string TabName { get; set; }
    Part SelectedPart { get; set; }
    void ZoomAll();
    void RefreshRoot();
    ObservableCollection<INodeDataGroup> NodeGroups { get; set; }
    void Clear();
    ObservableCollection<DiagramThemeGroup> DiagramThemeGroups { get; set; }
    ObservableCollection<FlowableDiagramTemplateNodeData> ReferenceTemplateNodeDatas { get; set; }
}
