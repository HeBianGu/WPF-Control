// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Diagram.Parts;

namespace H.Controls.Diagram.Bevaviors.NodeDataDropBehavior;

public class NodeDropReplaceNodeDataBevaior : DropNodeDataBehaviorBase<Node>
{
    protected override void OnAttached()
    {
        this.AssociatedObject.DragEnter += this.AssociatedObject_DragEnter;
        this.AssociatedObject.DragLeave += this.AssociatedObject_DragLeave;
        this.AssociatedObject.Drop += this.AssociatedObject_Drop;
        this.AssociatedObject.DragOver += this.AssociatedObject_DragOver;
    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.DragEnter -= this.AssociatedObject_DragEnter;
        this.AssociatedObject.DragLeave -= this.AssociatedObject_DragLeave;
        this.AssociatedObject.Drop -= this.AssociatedObject_Drop;
        this.AssociatedObject.DragOver -= this.AssociatedObject_DragOver;

    }

    private void AssociatedObject_DragOver(object sender, DragEventArgs e)
    {
        e.Effects = DragDropEffects.All;
    }

    private void AssociatedObject_Drop(object sender, DragEventArgs e)
    {
        this.OnDrop(e);
    }

    public static bool GetCanReplaceNode(DependencyObject obj)
    {
        return (bool)obj.GetValue(CanReplaceNodeProperty);
    }

    public static void SetCanReplaceNode(DependencyObject obj, bool value)
    {
        obj.SetValue(CanReplaceNodeProperty, value);
    }

    public static readonly DependencyProperty CanReplaceNodeProperty =
        DependencyProperty.RegisterAttached("CanReplaceNode", typeof(bool), typeof(NodeDropReplaceNodeDataBevaior), new PropertyMetadata(default(bool), OnCanReplaceNodeChanged));

    static public void OnCanReplaceNodeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d as DependencyObject;

        bool n = (bool)e.NewValue;

        bool o = (bool)e.OldValue;
    }

    private Node _cacheNode;
    private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
    {
        NodeDropReplaceNodeDataBevaior.SetCanReplaceNode(this.AssociatedObject, false);
        this.Cancel();
    }

    void Cancel()
    {
        var diagram = this.AssociatedObject.GetDiagram();
        if (diagram == null)
            return;
        if (this._cacheNode == null)
            return;
        diagram.RemoveNode(this._cacheNode);
        this._cacheNode = null;
    }


    private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
    {
        var nodeData = this.GetDragNodeData(e);
        if (nodeData == null)
            return;
        Node node = this.CreateNodeByData(nodeData);
        node.IsHitTestVisible = false;
        if (!this.AssociatedObject.IsStructEquatable(node))
        {
            e.Effects = DragDropEffects.None;
            this.AssociatedObject.AllowDrop = false;
            return;
        }
        node.Content = nodeData;
        node.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        node.Location = this.AssociatedObject.Location;
        var diagram = this.AssociatedObject.GetDiagram();
        if (diagram == null)
            return;
        diagram.AddNode(node);
        this._cacheNode = node;
        this.SetPreviewOpacity(0.7);
        NodeDropReplaceNodeDataBevaior.SetCanReplaceNode(this.AssociatedObject, true);
        diagram.Message = "请松开鼠标按钮替换节点";
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

    protected override void OnDropNodeData(INodeData nodeData, Point offset, Point location)
    {
        if(this._cacheNode==null) 
            return;
        var oldNode = this.AssociatedObject;
        var diagram = this.AssociatedObject.GetDiagram();
        if (diagram == null)
            return;
        diagram.ReplaceNode(oldNode, this._cacheNode);
        diagram.RemoveNode(oldNode);
        this._cacheNode.IsHitTestVisible = true;
        this.SetPreviewOpacity(1.0);
        this._cacheNode = null;
    }


}
