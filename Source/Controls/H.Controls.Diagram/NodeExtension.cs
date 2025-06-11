// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram;

public static class NodeExtension
{
    public static void AligmentToNodesWithStartNode(this IEnumerable<Node> nodes)
    {
        //Action<IEnumerable<Node>> action = null;
        //action = x =>
        //{
        //    foreach (Node node in x)
        //    {
        //        node.AligmentLayout();
        //        List<Node> fromNodes = node.GetToNodes();
        //        action.Invoke(fromNodes);
        //    }
        //};
        IEnumerable<Node> ns = nodes.Where(x => x.LinksInto.Count == 0);
        ns.AligmentToNodes();
        //action.Invoke(ns);
    }

    public static void AligmentToNodes(this IEnumerable<Node> nodes)
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
        action.Invoke(nodes);
    }

    public static void AligmentToNodes(this Node node)
    {
        AligmentToNodes(new List<Node>() { node });
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
