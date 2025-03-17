using H.Controls.Diagram.GraphSource;

namespace H.Controls.Diagram.Presenter.Provider;

public class DiagramFlowableDataSource : DiagramDataSource
{
    public DiagramFlowableDataSource(List<Node> nodeSource) : base(nodeSource)
    {
    }

    public DiagramFlowableDataSource(IEnumerable<INodeData> nodes, IEnumerable<ILinkData> links) : base(nodes, links)
    {

    }

    protected override ILinkData CreateLinkData()
    {
        return new FlowableLinkData();
    }

}
