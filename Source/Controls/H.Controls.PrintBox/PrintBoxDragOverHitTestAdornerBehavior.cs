// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Interfaces;
using H.Controls.Adorner.Draggable;
using H.Extensions.Behvaiors.Adorners;
using H.Presenters.Design.Base;
using H.Presenters.Design.PrintPresenter;
using System.Collections;
using System.Windows.Documents;

namespace H.Controls.PrintBox
{
    //public class PrintBoxDragOverHitTestAdornerBehavior : DragOverHitTestAdornerBehavior
    //{
    //    public bool UsePreView
    //    {
    //        get { return (bool)GetValue(UsePreViewProperty); }
    //        set { SetValue(UsePreViewProperty, value); }
    //    }

    //    public static readonly DependencyProperty UsePreViewProperty =
    //        DependencyProperty.Register("UsePreView", typeof(bool), typeof(PrintBoxDragOverHitTestAdornerBehavior), new FrameworkPropertyMetadata(default(bool), (d, e) =>
    //        {
    //            PrintBoxDragOverHitTestAdornerBehavior control = d as PrintBoxDragOverHitTestAdornerBehavior;

    //            if (control == null) return;

    //            if (e.OldValue is bool o)
    //            {

    //            }

    //            if (e.NewValue is bool n)
    //            {

    //            }
    //        }));

    //    protected override void DragEnter(UIElement element, DragEventArgs e)
    //    {
    //        if (this.UsePreView)
    //        {
    //            this.Add(element, e);
    //        }
    //        else
    //        {
    //            base.DragEnter(element, e);
    //        }
    //    }

    //    protected override void DragLeave(UIElement element, DragEventArgs e)
    //    {
    //        if (this.UsePreView)
    //        {
    //            this.Remove(element, e);
    //        }
    //        else
    //        {
    //            base.DragLeave(element, e);
    //        }
    //    }

    //    protected override void Drop(object sender, DragEventArgs e)
    //    {
    //        if (this.UsePreView)
    //            return;
    //        this.Add(_preVisualHitElement, e);
    //    }

    //    void Add(UIElement element, DragEventArgs e)
    //    {
    //        if (e.Effects == DragDropEffects.Move)
    //        {
    //            this.Remove(element, e);
    //        }
    //        System.Diagnostics.Debug.WriteLine("Add");
    //        IDraggableAdorner adorner = e.Data.GetData("DragGroup") as IDraggableAdorner;
    //        var data = adorner.GetData();
    //        if (data is IPrintPagePresenter page)
    //        {
    //            if (page is ICloneable c)
    //                this.AssociatedObject.GetItemsSource<IList>().Add(c.Clone());
    //            return;
    //        }

    //        var econtent = element.GetContent();
    //        var value = data is ICloneable cloneable ? cloneable.Clone() as IDesignPresenter : data as IDesignPresenter;
    //        if (econtent is IItemsControlPrintPagePresenter listPrintPage)
    //        {
    //            listPrintPage.Presenters.Add(value);
    //            return;
    //        }

    //        if (econtent is IContentPrintPagePresenter contentPrintPage)
    //        {
    //            contentPrintPage.Content= value;
    //            return;
    //        }

    //        if (econtent is IHitTestElementDropable drag)
    //        {
    //            if (drag.CanDrop(element, e))
    //                drag.Drop(element, e);
    //            return;
    //        }

    //        if (element == null)
    //            return;
    //        var pp = element.GetParent<ItemsControl>();
    //        if (pp == null)
    //            return;
    //        if (pp.ItemsSource is IList list)
    //        {
    //            var content = element.GetContent();
    //            int index = list.IndexOf(content);
    //            if (index != -1)
    //            {
    //                list.Insert(index, value);
    //            }
    //        }
    //    }

    //    void Remove(UIElement from, DragEventArgs e)
    //    {
    //        System.Diagnostics.Debug.WriteLine("Remove");
    //        IDraggableAdorner adorner = e.Data.GetData("DragGroup") as IDraggableAdorner;
    //        var data = adorner.GetData() as IDesignPresenter;
    //        var element = (adorner as System.Windows.Documents.Adorner).AdornedElement;
    //        if (element == from)
    //            return;
    //        if (data is IPrintPagePresenter page)
    //        {
    //            this.AssociatedObject.GetItemsSource<IList>().Remove(page);
    //            return;
    //        }

    //        if (element.GetContent() is IChildableDesignPresenter listPrintPage)
    //        {
    //            listPrintPage.Delete(data);
    //            return;
    //        }
    //        if (element.GetContent() is IHitTestElementDragable drag)
    //        {
    //            drag.DragLeave(element, e);
    //            return;
    //        }
    //        if (element == null)
    //            return;
    //        var pp = element.GetParent<ItemsControl>();
    //        if (pp == null)
    //            return;
    //        if (pp.ItemsSource is IList list)
    //            list.Remove(data);

    //    }

    //    //protected override Adorner GetAdorner(UIElement elment)
    //    //{
    //    //    if (elment.GetContent() is ListPrintPagePresenter listPrintPagePresenter)
    //    //    {
    //    //        return new LineAdorner(elment) { Dock = Dock.Bottom };
    //    //    }
    //    //    return base.GetAdorner(elment);
    //    //}
    //}
}
