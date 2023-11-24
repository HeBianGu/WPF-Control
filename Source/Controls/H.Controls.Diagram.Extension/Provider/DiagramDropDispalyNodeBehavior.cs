﻿

using System;
using System.Windows;


namespace H.Controls.Diagram.Extension
{
    public class DiagramDropTextNodeBehavior : DiagramDropBehaviorBase
    {
        public Type NodeType
        {
            get { return (Type)GetValue(NodeTypeProperty); }
            set { SetValue(NodeTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NodeTypeProperty =
            DependencyProperty.Register("NodeType", typeof(Type), typeof(DiagramDropTextNodeBehavior), new FrameworkPropertyMetadata(typeof(Node), (d, e) =>
             {
                 DiagramDropTextNodeBehavior control = d as DiagramDropTextNodeBehavior;

                 if (control == null) return;

                 if (e.OldValue is Type o)
                 {

                 }

                 if (e.NewValue is Type n)
                 {

                 }

             }));


        protected override Node Create(INodeData nodeData)
        {
            ISystemNodeData componentNode = nodeData as ISystemNodeData;
            Node node = Activator.CreateInstance(this.NodeType) as Node;
            foreach (var p in componentNode.PortDatas)
            {
                Port port = Port.Create(node);
                port.Dock = p.Dock;
                //port.Visibility = Visibility.Collapsed;
                port.PortType = p.PortType;
                port.Content = p;
                port.Margin = p.PortMargin;
                node.AddPort(port);
            }

            if (nodeData is ITemplate template)
            {
                template.IsTemplate = false;
            }

            return node;
        }
    }
}