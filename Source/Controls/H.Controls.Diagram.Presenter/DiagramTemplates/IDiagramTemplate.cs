using H.Common.Interfaces;

namespace H.Controls.Diagram.Presenter.DiagramTemplates;

public interface IDiagramTemplate : INameable, IGroupable
{
    public IDiagramData Diagram { get; set; }
}
