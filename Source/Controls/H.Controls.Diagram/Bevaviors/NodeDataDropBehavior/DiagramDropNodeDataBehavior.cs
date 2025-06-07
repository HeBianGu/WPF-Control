// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Bevaviors.NodeDataDropBehavior;

public class DiagramDropNodeDataBehavior : DropNodeDataBehaviorBase<Diagram>
{
    public bool UseAutoAddLinkOnEnd
    {
        get { return (bool)GetValue(UseAutoAddLinkOnEndProperty); }
        set { SetValue(UseAutoAddLinkOnEndProperty, value); }
    }

    public static readonly DependencyProperty UseAutoAddLinkOnEndProperty =
        DependencyProperty.Register("UseAutoAddLinkOnEnd", typeof(bool), typeof(DiagramDropNodeDataBehavior), new FrameworkPropertyMetadata(true));


    protected override void OnDropNodeData(INodeData nodeData, Point offset, Point location)
    {
        Node node = this.CreateNodeByData(nodeData);
        node.Content = nodeData;
        node.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        node.Location = new Point(location.X - offset.X + node.DesiredSize.Width / 2, location.Y - offset.Y + node.DesiredSize.Height / 2);
        //collection.Add(node);
        //this.AssociatedObject.RefreshData();
        this.AssociatedObject.AddNode(node);
        if (this.UseAutoAddLinkOnEnd)
            this.AssociatedObject.LinkOnEnd(node);
    }
}
