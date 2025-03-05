// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
global using H.Controls.Diagram.Presenter.LinkDatas;
global using H.Controls.Diagram.Presenter.NodeDatas;
namespace H.Controls.Diagram.Presenter.Provider;

/// <summary>
/// 根据 Unit和Wire加载的数据源
/// </summary>
public class DisplayGraphSource : GraphSource<TextNodeData, FlowableLinkData>
{
    public DisplayGraphSource(List<Node> nodeSource) : base(nodeSource)
    {

    }

    public DisplayGraphSource(IEnumerable<TextNodeData> nodes, IEnumerable<FlowableLinkData> links) : base(nodes, links)
    {

    }

    protected override Node ConvertToNode(TextNodeData unit)
    {
        Node node = new Node() { Content = unit };
        //node.Id = unit.ID;
        node.Location = unit.Location;
        foreach (IPortData socket in unit.PortDatas)
        {
            Port port = Port.Create(node);
            //port.Id = socket.ID;
            port.Content = socket;
            port.Dock = socket.Dock;
            //port.Visibility = System.Windows.Visibility.Hidden;
            port.PortType = socket.PortType;
            node.AddPort(port);
        }

        return node;
    }

    [Obsolete]
    protected override Link ConvertToLink(FlowableLinkData wire)
    {
        Node fromNode = this.NodeSource.FirstOrDefault(l => l.Id == wire.FromNodeID);
        Node toNode = this.NodeSource.FirstOrDefault(l => l.Id == wire.ToNodeID);
        Port fromPort = fromNode.GetPorts().FirstOrDefault(l => l.Id == wire.FromPortID);
        Port toPort = toNode.GetPorts().FirstOrDefault(l => l.Id == wire.ToPortID);
        Link result = Link.Create(fromNode, toNode, fromPort, toPort);
        result.Content = wire;
        return result;
    }

    public override List<TextNodeData> GetNodeType()
    {
        List<TextNodeData> result = new List<TextNodeData>();
        foreach (Node node in this.NodeSource)
        {
            TextNodeData unit = node.Content as TextNodeData;
            unit.Location = NodeLayer.GetPosition(node);
            List<IPortData> ports = new List<IPortData>();
            foreach (Port port in node.GetPorts())
            {
                TextPortData defaultPort = port.Content as TextPortData;
                defaultPort.Dock = port.Dock;
                ports.Add(defaultPort);
            }
            unit.PortDatas = ports;
            result.Add(unit);
        }
        return result;
    }

    public override List<FlowableLinkData> GetLinkType()
    {
        List<FlowableLinkData> result = new List<FlowableLinkData>();
        foreach (Node node in this.NodeSource)
        {
            foreach (Link link in node.LinksOutOf)
            {
                INodeData from = link.FromNode?.Content as INodeData;
                INodeData to = link.ToNode?.Content as INodeData;
                FlowableLinkData wire = link.Content as FlowableLinkData;
                //wire.RefreshData(from,to);

                wire.FromNodeID = from?.ID;
                wire.ToNodeID = to?.ID;
                wire.FromPortID = (link.FromPort?.Content as IPortData)?.ID;
                wire.ToPortID = (link.ToPort?.Content as IPortData)?.ID;
                result.Add(wire);
            }
        }
        return result;
    }
}
