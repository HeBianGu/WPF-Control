// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.IO;

namespace H.Controls.Diagram.Bevaviors.NodeDataDropBehavior;

public class PortDropNodeDataBevaior : DropNodeDataBehaviorBase<Port>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        this.AssociatedObject.DragOver += this.AssociatedObject_DragOver;
        this.AssociatedObject.DragEnter += this.AssociatedObject_DragEnter;
        this.AssociatedObject.DragLeave += this.AssociatedObject_DragLeave;
    }


    protected override void OnDetaching()
    {
        base.OnDetaching();
        this.AssociatedObject.DragOver -= this.AssociatedObject_DragOver;
        this.AssociatedObject.DragEnter -= this.AssociatedObject_DragEnter;
        this.AssociatedObject.DragLeave -= this.AssociatedObject_DragLeave;
    }

    protected override void OnDrop(DragEventArgs e)
    {
        //base.OnDrop(e);
        e.Handled = true;
        this.SetPreviewOpacity(1.0);
        this._cacheNode = null;
        PortDropNodeDataBevaior.SetIsDragOver(this.AssociatedObject, false);
    }

    private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
    {
        this.Cancel();
        e.Handled = true;
        PortDropNodeDataBevaior.SetIsDragOver(this.AssociatedObject, false);
    }

    private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
    {
        this.Sumit(e);
        this.SetPreviewOpacity(0.2);
        e.Handled = true;
        PortDropNodeDataBevaior.SetIsDragOver(this.AssociatedObject, true);
    }

    private void SetPreviewOpacity(double opacity = 0.2)
    {
        if (this._cacheNode == null)
            return;
        this._cacheNode.Opacity = opacity;
        foreach (var item in this._cacheNode.GetParts())
        {
            item.Opacity = opacity;
        }
    }

    void Cancel()
    {
        var diagram = this.AssociatedObject.GetDiagram();
        if (diagram == null)
            return;
        if (this._cacheNode == null)
            return;
        diagram.RemoveNode(this._cacheNode);
    }

    private void AssociatedObject_DragOver(object sender, DragEventArgs e)
    {
        if (this.AssociatedObject.PortType == PortType.Input)
        {
            e.Effects = DragDropEffects.None;
            this.AssociatedObject.AllowDrop = false;
        }
        e.Handled = true;
    }

    private Node _cacheNode;
    protected override void OnDropNodeData(INodeData nodeData, Point offset, Point location)
    {
        Node node = this.CreateNodeByData(nodeData);
        node.Content = nodeData;
        node.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        node.Location = new Point(location.X - offset.X + node.DesiredSize.Width / 2, location.Y - offset.Y + node.DesiredSize.Height / 2);
        var fromPort = this.AssociatedObject;
        var diagram = this.AssociatedObject.GetDiagram();
        if (diagram == null)
            return;
        var toNodes = fromPort.GetLinksOutOf().Select(x => x.ToNode).ToList();
        diagram.AddNode(node);
        diagram.LinkNode(fromPort, node);
        node.AligmentLayout();
        if (toNodes.Count > 0)
        {
            if (fromPort.Dock == Dock.Right)
            {
                var y = toNodes.Select(x => x.Location.Y).Max();
                node.Location = new Point(node.Location.X, y + node.DesiredSize.Height + 30);
            }
            if (fromPort.Dock == Dock.Bottom)
            {
                var x = toNodes.Select(x => x.Location.X).Max();
                node.Location = new Point(x + node.DesiredSize.Width + 50, node.Location.Y);
            }
        }
        diagram.RefreshLayout();
        this._cacheNode = node;
    }



    public static bool GetIsDragOver(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsDragOverProperty);
    }

    public static void SetIsDragOver(DependencyObject obj, bool value)
    {
        obj.SetValue(IsDragOverProperty, value);
    }

    public static readonly DependencyProperty IsDragOverProperty =
        DependencyProperty.RegisterAttached("IsDragOver", typeof(bool), typeof(PortDropNodeDataBevaior), new PropertyMetadata(default(bool), OnIsDragOverChanged));

    static public void OnIsDragOverChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d as DependencyObject;

        bool n = (bool)e.NewValue;

        bool o = (bool)e.OldValue;
    }


}
