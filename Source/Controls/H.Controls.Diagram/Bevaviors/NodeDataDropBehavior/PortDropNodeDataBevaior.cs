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

    }

    private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
    {
        this.Cancel();
    }

    private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
    {
        this.Sumit(e);
        this.SetPreviewOpacity(0.2);
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
        diagram.AddNode(node);
        diagram.LinkNode(fromPort, node);
        node.AligmentLayout();
        this._cacheNode = node;
    }


}
