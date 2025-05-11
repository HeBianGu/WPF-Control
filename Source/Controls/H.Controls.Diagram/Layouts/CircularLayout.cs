// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Layouts;

/// <summary> 圆形布局 </summary>
[DisplayName("CircularLayout")]
[TypeConverter(typeof(DisplayNameConverter))]
public class CircularLayout : Layout
{
    public double Len { get; set; } = 200;

    public override void DoLayout(params Node[] nodes)
    {
        if (nodes == null) return;

        double height = this.Diagram.ActualHeight;

        double width = this.Diagram.ActualWidth;

        Point center = new Point(width / 2, height / 2);

        double span = 360.0 / nodes.Length;

        //  Do ：布局Node
        for (int i = 0; i < nodes.Length; i++)
        {
            Node node = nodes[i];

            double angle = i * span;

            Point start = new Point(center.X - this.Len, center.Y);

            Matrix matrix = new Matrix();

            matrix.RotateAt(angle, center.X, center.Y);

            Point end = matrix.Transform(start);

            //  Do ：设置节点
            NodeLayer.SetPosition(node, end);

            this.DoLayoutNode(node);

            this.DoLayoutPort(node);

            this.DoLayoutLink(node);

        }
    }
}
