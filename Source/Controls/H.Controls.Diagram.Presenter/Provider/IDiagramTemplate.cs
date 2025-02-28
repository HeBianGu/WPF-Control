using H.Controls.Diagram.Presenter.DiagramDatas.Base;

namespace H.Controls.Diagram.Presenter.Provider;

public interface IDiagramTemplate
{
    public string Name { get; set; }

    public string GroupName { get; set; }

    public string TypeName { get; set; }

    public IDiagramData Diagram { get; set; }
}
