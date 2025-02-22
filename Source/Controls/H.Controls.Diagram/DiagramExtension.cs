// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace H.Controls.Diagram
{
    public static class NodeExtension
    {
        public static void Aligment(this IEnumerable<Node> nodes)
        {
            Action<IEnumerable<Node>> action = null;
            action = x =>
            {
                foreach (Node node in x)
                {
                    node.Aligment();
                    List<Node> fromNodes = node.GetFromNodes();
                    action.Invoke(fromNodes);
                }
            };
            IEnumerable<Node> ns = nodes.Where(x => x.LinksOutOf.Count == 0);
            action.Invoke(ns);
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

    public static class DiagramExtension
    {
        public static void AligmentNodes(this Diagram diagram)
        {
            diagram.Nodes.Aligment();
        }

        public static Dock GetRevertDock(this Dock dock)
        {
            if (dock == Dock.Left) return Dock.Right;
            if (dock == Dock.Right) return Dock.Left;
            if (dock == Dock.Top) return Dock.Bottom;
            return Dock.Top;
        }
        public static void LinkNodes(this Diagram diagram, Node from, Node to, Dock dock = Dock.Bottom)
        {
            Link link = from.CreateLinkTo(to, from.GetPorts(x => x.Dock == dock).FirstOrDefault(),
                to.GetPorts(x => x.Dock == dock.GetRevertDock()).FirstOrDefault());
            diagram.AddLink(link);
        }
    }
}
