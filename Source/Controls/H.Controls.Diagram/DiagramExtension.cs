// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Controls.Diagram.Datas;
using H.Controls.Diagram.Parts;
using System.Windows.Controls;

namespace H.Controls.Diagram;

public static class NodeExtension
{
    public static void Aligment(this IEnumerable<Node> nodes)
    {
        Action<IEnumerable<Node>> action = null;
        action = x =>
        {
            foreach (Node node in x)
            {
                node.AligmentLayout();
                List<Node> fromNodes = node.GetToNodes();
                action.Invoke(fromNodes);
            }
        };
        IEnumerable<Node> ns = nodes.Where(x => x.LinksInto.Count == 0);
        action.Invoke(ns);
    }

    public static Part SelectPreivewPart(this Part part)
    {
        var find = part.GetPrevious();
        part.IsSelected = false;
        find.IsSelected = true;
        return find;
    }

    public static Part SelectNextPart(this Part part)
    {
        var find = part.GetNext();
        part.IsSelected = false;
        find.IsSelected = true;
        return find;
    }

    public static Link CreateLinkToNodes(this Node fromNode, Node toNode)
    {
        return fromNode == null || toNode == null ? null : fromNode.CreateLinkTo(toNode, null, null);
    }

    public static Link CreateLinkToPorts(this Node fromNode, Node toNode, Port fromPort, Port toPort)
    {
        return fromPort == null || toPort == null ? null : fromNode.CreateLinkTo(toNode, fromPort, toPort);
    }
    public static Link CreateLinkTo(this Node fromNode, Node toNode, Port fromPort, Port toPort)
    {
        Link link = new Link();
        link.FromNode = fromNode;
        link.ToNode = toNode;
        link.FromPort = fromPort;
        link.ToPort = toPort;
        if (fromNode.Content is ILinkInitializer initializer1)
            initializer1.InitLink(link);
        if (fromPort?.Content is ILinkInitializer initializer2)
            initializer2.InitLink(link);
        if (toNode.Content is ILinkInitializer initializer3)
            initializer3.InitLink(link);
        if (toPort?.Content is ILinkInitializer initializer4)
            initializer4.InitLink(link);

        if (fromNode.Content is ILinkDataCreator creator1)
            link.Content = creator1.CreateLinkData();
        if (fromPort?.Content is ILinkDataCreator creator)
            link.Content = creator.CreateLinkData();
        if (link.Content is IFlowable)
        {
            fromNode.LinksOutOf.Add(link);
            toNode.LinksInto.Add(link);
        }
        else
        {
            fromNode.ConnectLinks.Add(link);
            toNode.ConnectLinks.Add(link);
        }
        return link;
    }
}
public static class DockExtension
{
    public static Dock GetRevert(this Dock dock)
    {
        if (dock == Dock.Left) return Dock.Right;
        if (dock == Dock.Right) return Dock.Left;
        return dock == Dock.Top ? Dock.Bottom : Dock.Top;
    }

    public static Thickness GetTextMargin(this Dock dock, int offset = -30)
    {
        //return new Thickness(0, 0, 0, 0);
        if (dock == Dock.Left) return new Thickness(offset, 0, 0, 0);
        if (dock == Dock.Right) return new Thickness(0, 0, offset, 0);
        return dock == Dock.Top ? new Thickness(0, offset, 0, 0) : new Thickness(0, 0, 0, offset);
    }

    public static string GetIcon(this IPortData portData)
    {
        string down = "\xE74B";
        string up = "\xE74A";
        string left = "\xE72B";
        string right = "\xE72A";
        if (portData.PortType == PortType.Both)
            return null;
        if (portData.Dock == Dock.Top)
            return portData.PortType == PortType.Input ? down : up;
        else if (portData.Dock == Dock.Bottom)
            return portData.PortType == PortType.Input ? up : down;
        else if (portData.Dock == Dock.Left)
            return portData.PortType == PortType.Input ? right : left;
        else if (portData.Dock == Dock.Right)
            return portData.PortType == PortType.Input ? left : right;
        return null;
    }

}
public static class DiagramExtension
{
    public static void AligmentNodes(this Diagram diagram)
    {
        var starts = diagram.Nodes.Where(x => x.LinksInto.Count == 0);
        starts.Aligment();
    }

    public static void LinkNode(this Diagram diagram, Node from, Node to, Dock dock = Dock.Bottom)
    {
        Port fromPort = from.GetPorts(x => x.Dock == dock && x.PortType.HasFlag(PortType.OutPut)).FirstOrDefault();
        Port toPort = to.GetPorts(x => x.Dock == dock.GetRevert() && x.PortType.HasFlag(PortType.Input)).FirstOrDefault();

        if (fromPort == null && toPort != null)
            return;
        if (fromPort != null && toPort == null)
            return;
        Link link = from.CreateLinkTo(to, fromPort, toPort);
        diagram.AddLink(link);
    }

    public static void LinkNode(this Diagram diagram, Port fromPort, Node to)
    {
        if (fromPort == null)
            return;
        Port toPort = to.GetPorts(x => x.Dock == fromPort.Dock.GetRevert() && x.PortType.HasFlag(PortType.Input)).FirstOrDefault();
        if (fromPort == null && toPort != null)
            return;
        if (fromPort != null && toPort == null)
            return;
        Link link = fromPort.ParentNode.CreateLinkTo(to, fromPort, toPort);
        diagram.AddLink(link);
    }

    public static void LinkPort(this Diagram diagram, Port fromPort, Port toPort)
    {
        if (fromPort == null && toPort != null)
            return;
        if (fromPort != null && toPort == null)
            return;
        var to = toPort.ParentNode;
        Link link = fromPort.ParentNode.CreateLinkTo(to, fromPort, toPort);
        diagram.AddLink(link);
    }

    public static void RemoveLink(this Diagram diagram, Link link)
    {
        diagram.LinkLayer.Children.Remove(link);
        diagram?.OnItemsChanged();
    }

    public static void InsertLinkNode(this Diagram diagram, Link link, Node node)
    {
        var fromPort = link.FromPort;
        var toPort = link.ToPort;
        if (fromPort == null)
            return;
        if (toPort == null)
            return;
        var outputPorts = node.GetPorts(x => x.PortType.HasFlag(PortType.OutPut));
        var nodeFromPort = outputPorts.Where(x => x.Dock == fromPort.Dock.GetRevert()).FirstOrDefault();
        if (nodeFromPort == null)
            nodeFromPort = outputPorts.FirstOrDefault();
        if (nodeFromPort == null)
            return;
        diagram.RemoveLink(link);
        diagram.AddNode(node);
        diagram.LinkNode(fromPort, node);
        diagram.LinkPort(nodeFromPort, toPort);
        diagram.AligmentNodes();
    }

    public static void ZoomToFit(this IDiagram diagram, params Part[] parts)
    {
        diagram.ZoomToFit(1.8, parts);
    }

    public static void LinkOnEnd(this Diagram diagram, Node node, Dock dock = Dock.Bottom)
    {
        List<Node> endNodes = diagram.Nodes.Where(x => x != node && x.GetPorts(x => x.PortType == PortType.OutPut && x.Dock == dock && x.GetLinksOutOf().Count() == 0).Count > 0).ToList();
        if (endNodes.Count == 1)
        {
            Node firstFrom = endNodes.First();
            diagram.LinkNode(firstFrom, node, dock);
            diagram.AligmentNodes();
        }
    }
}
