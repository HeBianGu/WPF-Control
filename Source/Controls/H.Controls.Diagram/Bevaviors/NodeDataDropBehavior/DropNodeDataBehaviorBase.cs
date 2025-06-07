// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Adorner.Draggable;
using Microsoft.Xaml.Behaviors;

namespace H.Controls.Diagram.Bevaviors.NodeDataDropBehavior;

public abstract class DropNodeDataBehaviorBase<T> : Behavior<T> where T : UIElement
{
    protected override void OnAttached()
    {
        this.AssociatedObject.Drop += AssociatedObject_Drop;
    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.Drop -= AssociatedObject_Drop;
    }

    private void AssociatedObject_Drop(object sender, DragEventArgs e)
    {
        this.OnDrop(e);
    }

    protected virtual void OnDrop(DragEventArgs e)
    {
        this.Sumit(e);
        e.Handled = true;
    }

    protected void Sumit(DragEventArgs e)
    {
        IDraggableAdorner adorner = e.Data.GetData("DragGroup") as IDraggableAdorner;
        if (adorner == null)
            return;
        Point offset = adorner.Offset;
        Point location = e.GetPosition(this.AssociatedObject);
        ICloneable obj = adorner.GetData() as ICloneable;
        if (obj == null)
            throw new ArgumentException("没有实现ICloneable接口");
        INodeData nodeData = obj.Clone() as INodeData;
        this.OnDropNodeData(nodeData, offset, location);
    }

    protected abstract void OnDropNodeData(INodeData nodeData, Point offset, Point location);

    protected virtual Node CreateNodeByData(INodeData nodeData)
    {
        IPortableNodeData componentNode = nodeData as IPortableNodeData;
        Node node = this.CreateNode();
        foreach (IPortData p in componentNode.PortDatas)
        {
            Port port = Port.Create(node);
            port.Dock = p.Dock;
            port.PortType = p.PortType;
            port.Content = p;
            port.Margin = p.PortMargin;
            node.AddPort(port);
        }
        if (nodeData is ITemplate template)
            template.IsTemplate = false;
        return node;
    }

    protected virtual Node CreateNode() => new Node();
}
