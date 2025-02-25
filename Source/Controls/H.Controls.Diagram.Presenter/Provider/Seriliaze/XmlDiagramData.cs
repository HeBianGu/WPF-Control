namespace H.Controls.Diagram.Presenter.Provider.Seriliaze;

public class XmlDiagramData
{
    public List<INodeData> Nodes { get; set; } = new List<INodeData>();
    public List<ILinkData> Links { get; set; } = new List<ILinkData>();
    public List<XmlStringData> ReferenceTemplates { get; set; } = new List<XmlStringData>();
}
