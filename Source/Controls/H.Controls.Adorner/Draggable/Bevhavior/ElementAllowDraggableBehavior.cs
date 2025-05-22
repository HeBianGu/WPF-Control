// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Attach;
using System.Collections;

namespace H.Controls.Adorner.Draggable.Bevhavior;

/// <summary> 用于显示ListBoxItem拖拽进行时的效果，通过附加属性Bool修改样式 </summary>
public class ElementAllowDraggableBehavior : DraggableAdornerBehavior
{
    protected override void OnAttached()
    {
        this.AssociatedObject.AllowDrop = true;

        this.AssociatedObject.Drop += AssociatedObjectOnDrop;

        this.AssociatedObject.DragEnter += AssociatedObject_DragEnter;

        this.AssociatedObject.DragLeave += AssociatedObject_DragLeave;

        base.OnAttached();

    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.AllowDrop = false;

        this.AssociatedObject.Drop -= AssociatedObjectOnDrop;

        this.AssociatedObject.DragEnter -= AssociatedObject_DragEnter;

        this.AssociatedObject.DragLeave -= AssociatedObject_DragLeave;

        base.OnDetaching();
    }

    protected override void DoDragDrop()
    {
        ItemsControl items = this.AssociatedObject.GetParent<ItemsControl>();

        if (items == null) return;

        if (items.ItemsSource == null)
        {
            DataObject dragData = new DataObject(this.DragGroup, this.AssociatedObject is ListBoxItem item ? item.Content : this.AssociatedObject);

            int index = items.Items.IndexOf(this.AssociatedObject);

            DragDropEffects result = DragDrop.DoDragDrop(this.AssociatedObject, dragData, this.DragDropEffects);

            //  Do ：移动成功清理数据
            if (result == DragDropEffects.Move)
            {
                if (this.AssociatedObject is FrameworkElement frameworkElement)
                {
                    int index1 = items.Items.IndexOf(this.AssociatedObject);

                    if (index == index1)
                        //  Do ：下移 
                        items.Items.RemoveAt(index);
                    else
                    {
                        //  Do ：上移
                        items.Items.RemoveAt(index + 1);
                    }
                }
            }
        }
        else
        {
            object data = (this.AssociatedObject as FrameworkElement).DataContext;

            DataObject dragData = new DataObject(this.DragGroup, (this.AssociatedObject as FrameworkElement).DataContext);

            int index = (items.ItemsSource as IList).IndexOf(data);

            DragDropEffects result = DragDrop.DoDragDrop(this.AssociatedObject, dragData, this.DragDropEffects);

            //  Do ：移动成功清理数据

            if (result == DragDropEffects.Move)
            {
                if (this.AssociatedObject is FrameworkElement frameworkElement)
                {
                    int index1 = (items.ItemsSource as IList).IndexOf(data);

                    if (index == index1)
                        //  Do ：下移 
                        (items.ItemsSource as IList).RemoveAt(index);
                    else
                    {
                        //  Do ：上移
                        (items.ItemsSource as IList).RemoveAt(index + 1);
                    }
                }
            }
        }
    }

    private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
    {
        Cattach.SetIsDragEnter(this.AssociatedObject, false);
    }

    private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
    {
        Cattach.SetIsDragEnter(this.AssociatedObject, true);
    }

    private void AssociatedObjectOnDrop(object sender, DragEventArgs dragEventArgs)
    {
        Cattach.SetIsDragEnter(this.AssociatedObject, false);
    }
}

