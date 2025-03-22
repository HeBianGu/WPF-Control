using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace H.Controls.Diagram.GraphSource;

public class DiagramDataSource : GraphSource<INodeData, ILinkData>, IDiagramDataSource
{
    public DiagramDataSource(List<Node> nodeSource) : base(nodeSource)
    {

    }

    public DiagramDataSource(IEnumerable<INodeData> nodes, IEnumerable<ILinkData> links) : base(nodes, links)
    {

    }

    protected override Node ConvertToNode(INodeData unit)
    {
        Node node = new Node() { Content = unit };
        //node.Id = unit.ID;
        node.Location = unit.Location;
        if (unit is IPortableNodeData portData)
        {
            foreach (IPortData pd in portData.PortDatas)
            {
                Port port = Port.Create(node);
                //port.Id = socket.ID;
                port.Content = pd;
                port.Dock = pd.Dock;
                port.PortType = pd.PortType;
                port.Margin = pd.PortMargin;
                //port.Visibility = System.Windows.Visibility.Collapsed;
                node.AddPort(port);
            }
        }

        return node;
    }

    protected override Link ConvertToLink(ILinkData linkData)
    {
        Node fromNode = this.Nodes.FirstOrDefault(l => l.Id == linkData.FromNodeID);
        Node toNode = this.Nodes.FirstOrDefault(l => l.Id == linkData.ToNodeID);
        Port fromPort = fromNode.GetPorts(l => l.GetContent<IPortData>().ID == linkData.FromPortID)?.FirstOrDefault();
        Port toPort = toNode.GetPorts(l => l.Id == linkData.ToPortID)?.FirstOrDefault();
        Link result = fromNode.CreateLinkTo(toNode, fromPort, toPort);
        result.Content = linkData;
        return result;
    }

    public override List<INodeData> GetNodeDatas()
    {
        List<INodeData> result = new List<INodeData>();

        foreach (Node node in this.Nodes)
        {
            INodeData unit = node.GetContent<INodeData>(); ;
            IEnumerable<IPortData> sockets = node.GetPorts().Select(l => l.GetContent<IPortData>());
            if (unit is IPortableNodeData portableNodeData)
                portableNodeData.PortDatas = sockets.ToList();
            Application.Current.Dispatcher.Invoke(() =>
                 {
                     unit.Location = node.Location;
                 });
            result.Add(unit);
        }
        return result;
    }

    public override List<ILinkData> GetLinkDatas()
    {
        List<ILinkData> result = new List<ILinkData>();
        foreach (Node node in this.Nodes)
        {
            foreach (Link link in node.LinksOutOf.Concat(node.ConnectLinks))
            {
                ILinkData wire = link.GetContent<ILinkData>();
                wire.FromNodeID = link.FromNode?.GetContent<INodeData>()?.ID;
                wire.ToNodeID = link.ToNode?.GetContent<INodeData>()?.ID;
                wire.FromPortID = link.FromPort?.GetContent<IPortData>()?.ID;
                wire.ToPortID = link.ToPort?.GetContent<IPortData>()?.ID;
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
