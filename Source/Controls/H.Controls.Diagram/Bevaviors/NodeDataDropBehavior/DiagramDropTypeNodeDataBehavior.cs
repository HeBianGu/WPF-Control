// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Bevaviors.NodeDataDropBehavior;

public class DiagramDropTypeNodeDataBehavior : DiagramDropNodeDataBehavior
{
    public Type NodeType
    {
        get { return (Type)GetValue(NodeTypeProperty); }
        set { SetValue(NodeTypeProperty, value); }
    }

    public static readonly DependencyProperty NodeTypeProperty =
        DependencyProperty.Register("NodeType", typeof(Type), typeof(DiagramDropTypeNodeDataBehavior), new FrameworkPropertyMetadata(typeof(Node), (d, e) =>
        {
            DiagramDropTypeNodeDataBehavior control = d as DiagramDropTypeNodeDataBehavior;

            if (control == null) return;

            if (e.OldValue is Type o)
            {

            }

            if (e.NewValue is Type n)
            {

            }

        }));

    protected override Node CreateNode()
    {
        return Activator.CreateInstance(this.NodeType) as Node; ;
    }
}
