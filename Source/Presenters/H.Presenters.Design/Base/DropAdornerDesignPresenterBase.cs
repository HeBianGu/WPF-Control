// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Extensions.Behvaiors.Adorners;
global using System.Windows.Documents;
using H.Controls.Adorner.Adorner;

namespace H.Presenters.Design.Base;

public abstract class DropAdornerDesignPresenterBase : CommandsDesignPresenterBase, IHitTestElementDrag, IHitTestElementDrop
{
    public virtual Adorner GetDragAdorner(UIElement element)
    {
        return new LineAdorner(element);
    }

    public Adorner GetDropAdorner(UIElement element)
    {
        return new LineAdorner(element);
    }

    public virtual bool CanDrop(UIElement element, DragEventArgs e)
    {
        return true;
    }

    public virtual void Drop(UIElement element, DragEventArgs e)
    {
        //IDragAdorner adorner = e.Data.GetData("DragGroup") as IDragAdorner;
        //if (adorner.GetData() is ICloneable cloneable)
        //{
        //    object value = cloneable.Clone();
        //    var ic = element.GetParent<ItemsControl>();
        //    if (ic.ItemsSource is IList list)
        //        list.Add(e);
        //}

    }

    public virtual void DragEnter(UIElement element, DragEventArgs e)
    {

    }

    public virtual void DragLeave(UIElement element, DragEventArgs e)
    {

    }

    public virtual void DragOver(UIElement element, DragEventArgs e)
    {

    }

    public void RemoveDropAdorner(UIElement element)
    {

    }

    public void RemoveDragAdorner(UIElement element)
    {

    }

    public virtual bool IsHitTest(UIElement element, DragEventArgs e)
    {
        return true;
    }
}
