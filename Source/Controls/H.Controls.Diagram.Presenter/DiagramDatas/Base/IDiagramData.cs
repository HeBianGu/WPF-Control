namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public interface IDiagramData : ICloneable
{
    string ID { get; set; }
    string Name { get; set; }
    string GroupName { get; set; }
    string TypeName { get; set; }
    //string TabName { get; set; }
    Part SelectedPart { get; set; }
    void Clear();
}
