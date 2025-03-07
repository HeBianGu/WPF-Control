namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public interface IDiagramData : ICloneable, IDable, INameable, IGroupable
{
    Part SelectedPart { get; set; }
    void Clear();
}
