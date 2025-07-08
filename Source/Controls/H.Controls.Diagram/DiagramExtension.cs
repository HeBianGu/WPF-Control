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
public static class DiagramExtension
{
    public static void AligmentNodes(this Diagram diagram)
    {
        var starts = diagram.Nodes.Where(x => x.LinksInto.Count == 0);
        starts.AligmentToNodes();
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
        var nodeFromPort = outputPorts.Where(x => x.Dock == fromPort.Dock).FirstOrDefault();
        if (nodeFromPort == null)
            nodeFromPort = outputPorts.FirstOrDefault();
        if (nodeFromPort == null)
            return;

        var inputPorts = node.GetPorts(x => x.PortType.HasFlag(PortType.Input));
        var nodeToPort = inputPorts.Where(x => x.Dock == toPort.Dock).FirstOrDefault();
        if (nodeToPort == null)
            nodeToPort = inputPorts.FirstOrDefault();
        if (nodeToPort == null)
            return;
        diagram.AddNode(node);
        //diagram.LinkNode(fromPort, node);
        diagram.LinkPort(fromPort, nodeToPort);
        diagram.LinkPort(nodeFromPort, toPort);
        NodeLayer.SetPosition(node, link.ToNode.Location);
        //node.GetToNodes().AligmentToNodes();
        var tonodes = node.GetAllToNodes().ToList();
        Vector offset = NodeLayer.GetPosition(node) - NodeLayer.GetPosition(link.FromNode);
        foreach (var tonode in tonodes)
        {
            NodeLayer.SetPosition(tonode, NodeLayer.GetPosition(tonode) + offset);
        }
        diagram.RemoveLink(link);
        link.Delete();
        diagram.OnItemsChanged();
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
