// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Controls.Diagram.GraphSource.Model;
using System.Collections.Generic;
using System.Linq;

namespace H.Controls.Diagram.GraphSource;

/// <summary>
/// 根据 Unit和Wire加载的数据源
/// </summary>
public class UnitGraphSource : GraphSource<Unit, Wire>
{
    public UnitGraphSource(List<Unit> nodes, List<Wire> links) : base(nodes, links)
    {

    }

    protected override Node ConvertToNode(Unit unit)
    {
        Node node = new Node() { Content = unit };
        //node.Id = unit.Id;
        node.Location = new System.Windows.Point(unit.X, unit.Y);

        foreach (Socket socket in unit.Sockets)
        {
            Port port = Port.Create(node);
            //port.Id = socket.Name;
            port.Content = socket;
            port.Dock = socket.Dock;
        }

        return node;
    }

    [System.Obsolete]
    protected override Link ConvertToLink(Wire wire)
    {
        Node fromNode = this.NodeSource.FirstOrDefault(l => l.Id == wire.From);
        Node toNode = this.NodeSource.FirstOrDefault(l => l.Id == wire.To);

        Port fromPort = fromNode.GetPorts(l => l.Id == wire.FromPort)?.FirstOrDefault();
        Port toPort = fromNode.GetPorts(l => l.Id == wire.ToPort)?.FirstOrDefault();

        return Link.Create(fromNode, toNode, fromPort, toPort);
    }

    public override List<Unit> GetNodeType()
    {
        List<Unit> result = new List<Unit>();

        foreach (Node node in this.NodeSource)
        {
            Unit unit = node.Content as Unit;

            IEnumerable<Socket> sockets = node.GetPorts().Select(l => l.Content).Cast<Socket>();

            unit.Sockets = sockets.ToList();

            result.Add(unit);
        }

        return result;
    }

    public override List<Wire> GetLinkType()
    {
        List<Wire> result = new List<Wire>();

        foreach (Node node in this.NodeSource)
        {
            foreach (Link link in node.LinksOutOf)
            {
                Wire wire = new Wire();

                wire.From = link.FromNode?.Id;
                wire.To = link.ToNode?.Id;
                wire.FromPort = link.FromPort?.Id;
                wire.ToPort = link.ToPort?.Id;

                result.Add(wire);
            }
        }

        return result;
    }
}
