// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Controls.Diagram.Bevaviors.DiagramDropBehavior;

///// <summary>
///// 拖拽时放置Node类型
///// </summary>
//public class DiagramDropNodeBehavior : DiagramDropBehaviorBase
//{
//    protected override Node Create(INodeData nodeData)
//    { 
//        var node = new Node();

//        {
//            var port = Port.Create(node);
//            port.Dock = Dock.Left;
//            port.Id = "s_left";
//            port.Visibility = Visibility.Hidden;
//            port.PortType = PortType.Both;
//            port.Content = new DefaultPort(nodeData.ID);
//            node.AddPort(port);
//        }

//        {
//            var port = Port.Create(node);
//            port.Dock = Dock.Top;
//            port.Id = "s_top";
//            port.Visibility = Visibility.Hidden;
//            port.PortType = PortType.Both;
//            port.Content = new DefaultPort(nodeData.ID);
//            node.AddPort(port);

//        }

//        {
//            var port = Port.Create(node);
//            port.Dock = Dock.Bottom;
//            port.Id = "s_bottom";
//            port.Visibility = Visibility.Hidden;
//            port.PortType = PortType.Both;
//            port.Content = new DefaultPort(nodeData.ID);
//            node.AddPort(port);

//        }

//        {
//            var port = Port.Create(node);
//            port.Dock = Dock.Right;
//            port.Id = "s_right";
//            port.Visibility = Visibility.Hidden;
//            port.PortType = PortType.Both;
//            port.Content = new DefaultPort(nodeData.ID);
//            node.AddPort(port);

//        }

//        return node;
//    }
//}
