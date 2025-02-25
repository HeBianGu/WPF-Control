namespace H.Controls.Diagram.Presenter.Provider;

public class DiagramFlowableDataSourceConverter : DiagramDataSourceConverter
{
    public DiagramFlowableDataSourceConverter(List<Node> nodeSource) : base(nodeSource)
    {
    }

    public DiagramFlowableDataSourceConverter(IEnumerable<INodeData> nodes, IEnumerable<ILinkData> links) : base(nodes, links)
    {

    }

    protected override ILinkData CreateLinkData()
    {
        return new FlowableLinkData();
    }

}
