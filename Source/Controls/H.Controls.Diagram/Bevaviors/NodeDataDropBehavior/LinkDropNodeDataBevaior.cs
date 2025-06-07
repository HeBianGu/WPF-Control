// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Bevaviors.NodeDataDropBehavior;

public class LinkDropNodeDataBevaior : DropNodeDataBehaviorBase<Link>
{
    protected override void OnAttached()
    {
        this.AssociatedObject.PreviewDragEnter += this.AssociatedObject_DragEnter;
        this.AssociatedObject.PreviewDragLeave += this.AssociatedObject_DragLeave;
        this.AssociatedObject.PreviewDrop += this.AssociatedObject_PreviewDrop;
        this.AssociatedObject.PreviewDragOver += this.AssociatedObject_PreviewDragOver;
    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.PreviewDragEnter -= this.AssociatedObject_DragEnter;
        this.AssociatedObject.PreviewDragLeave -= this.AssociatedObject_DragLeave;
        this.AssociatedObject.PreviewDrop -= this.AssociatedObject_PreviewDrop;
        this.AssociatedObject.PreviewDragOver -= this.AssociatedObject_PreviewDragOver;

    }

    private void AssociatedObject_PreviewDragOver(object sender, DragEventArgs e)
    {
        e.Effects = DragDropEffects.All;
    }

    private void AssociatedObject_PreviewDrop(object sender, DragEventArgs e)
    {
        this.OnDrop(e);
    }

    public static bool GetCanInsertNode(DependencyObject obj)
    {
        return (bool)obj.GetValue(CanInsertNodeProperty);
    }

    public static void SetCanInsertNode(DependencyObject obj, bool value)
    {
        obj.SetValue(CanInsertNodeProperty, value);
    }

    public static readonly DependencyProperty CanInsertNodeProperty =
        DependencyProperty.RegisterAttached("CanInsertNode", typeof(bool), typeof(LinkDropNodeDataBevaior), new PropertyMetadata(default(bool), OnCanInsertNodeChanged));

    static public void OnCanInsertNodeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d as DependencyObject;

        bool n = (bool)e.NewValue;

        bool o = (bool)e.OldValue;
    }

    private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
    {
        LinkDropNodeDataBevaior.SetCanInsertNode(this.AssociatedObject, false);
    }

    private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
    {
        LinkDropNodeDataBevaior.SetCanInsertNode(this.AssociatedObject, true);
    }

    protected override void OnDropNodeData(INodeData nodeData, Point offset, Point location)
    {
        Node node = this.CreateNodeByData(nodeData);
        node.Content = nodeData;
        node.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        node.Location = new Point(location.X - offset.X + node.DesiredSize.Width / 2, location.Y - offset.Y + node.DesiredSize.Height / 2);
        var link = this.AssociatedObject;
        var diagram = this.AssociatedObject.GetDiagram();
        if (diagram == null)
            return;
        diagram.InsertLinkNode(link, node);
    }


}
