// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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
