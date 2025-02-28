global using H.Controls.Diagram.GraphSource;

namespace H.Controls.Diagram.Presenter.Provider;

public class DiagramDataSourceConverter : GraphSource<INodeData, ILinkData>
{
    public DiagramDataSourceConverter(List<Node> nodeSource) : base(nodeSource)
    {

    }

    public DiagramDataSourceConverter(IEnumerable<INodeData> nodes, IEnumerable<ILinkData> links) : base(nodes, links)
    {


    }

    protected override Node ConvertToNode(INodeData unit)
    {
        Node node = new Node() { Content = unit };
        //node.Id = unit.ID;
        node.Location = unit.Location;

        if (unit is IPortableNodeData systemNodeData)
        {
            foreach (IPortData socket in systemNodeData.PortDatas)
            {
                Port port = Port.Create(node);
                //port.Id = socket.ID;
                port.Content = socket;
                port.Dock = socket.Dock;
                port.Margin = socket.PortMargin;
                //port.Visibility = System.Windows.Visibility.Collapsed;
                node.AddPort(port);
            }
        }

        return node;
    }

    [Obsolete]
    protected override Link ConvertToLink(ILinkData wire)
    {
        Node fromNode = this.NodeSource.FirstOrDefault(l => l.Id == wire.FromNodeID);
        Node toNode = this.NodeSource.FirstOrDefault(l => l.Id == wire.ToNodeID);

        Port fromPort = fromNode.GetPorts(l => l.GetContent<IPortData>().ID == wire.FromPortID)?.FirstOrDefault();
        Port toPort = toNode.GetPorts(l => l.Id == wire.ToPortID)?.FirstOrDefault();

        Link result = Link.Create(fromNode, toNode, fromPort, toPort);
        result.Content = wire;
        return result;
    }

    public override List<INodeData> GetNodeType()
    {
        List<INodeData> result = new List<INodeData>();

        foreach (Node node in this.NodeSource)
        {
            INodeData unit = node.GetContent<INodeData>(); ;
            IEnumerable<IPortData> sockets = node.GetPorts().Select(l => l.GetContent<IPortData>());
            if (unit is IPortableNodeData systemNodeData)
                systemNodeData.PortDatas = sockets.ToList();

            Application.Current.Dispatcher.Invoke(() =>
                 {
                     unit.Location = node.Location;
                 });

            result.Add(unit);
        }
        return result;
    }

    public override List<ILinkData> GetLinkType()
    {
        List<ILinkData> result = new List<ILinkData>();
        foreach (Node node in this.NodeSource)
        {
            foreach (Link link in node.LinksOutOf.Concat(node.ConnectLinks))
            {
                ILinkData wire = link.GetContent<ILinkData>();
                wire.FromNodeID = link.FromNode?.GetContent<NodeData>()?.ID;
                wire.ToNodeID = link.ToNode?.GetContent<NodeData>()?.ID;
                wire.FromPortID = link.FromPort?.GetContent<PortData>()?.ID;
                wire.ToPortID = link.ToPort?.GetContent<PortData>()?.ID;
                result.Add(wire);
            }
        }
        return result;
    }

    protected virtual ILinkData CreateLinkData()
    {
        return new DefaultLinkData();
    }
}
