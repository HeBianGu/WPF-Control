// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Collections;

namespace H.Extensions.Behvaiors.DataGrids;

public class DataGridDropBehavior : Behavior<DataGrid>
{

    public static bool GetDragOver(DependencyObject obj)
    {
        return (bool)obj.GetValue(DragOverProperty);
    }

    public static void SetDragOver(DependencyObject obj, bool value)
    {
        obj.SetValue(DragOverProperty, value);
    }

    public static readonly DependencyProperty DragOverProperty =
        DependencyProperty.RegisterAttached("DragOver", typeof(bool), typeof(DataGridDropBehavior), new PropertyMetadata(default(bool), OnDragOverChanged));

    static public void OnDragOverChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d as DependencyObject;

        bool n = (bool)e.NewValue;

        bool o = (bool)e.OldValue;
    }


    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.Drop += this.AssociatedObject_Drop;
        AssociatedObject.DragOver += this.AssociatedObject_DragOver;
        AssociatedObject.GiveFeedback += this.AssociatedObject_GiveFeedback;
    }

    private void AssociatedObject_GiveFeedback(object sender, GiveFeedbackEventArgs e)
    {
        e.UseDefaultCursors = false;
        Mouse.SetCursor(Cursors.SizeNS);
        e.Handled = true;
    }

    private DataGridRow _preDataGridRow;
    private void AssociatedObject_DragOver(object sender, DragEventArgs e)
    {
        var point = e.GetPosition(this.AssociatedObject);
        DataGridRow dataGridRow = this.HitDataGridRow(point);
        this.Clear();
        if (dataGridRow != null)
        {
            SetDragOver(dataGridRow, true);
            _preDataGridRow = dataGridRow;
        }
    }

    private DataGridRow HitDataGridRow(Point point)
    {
        return this.AssociatedObject.HitTest<DataGridRow>(point, x => true);
    }
    private void Clear()
    {
        if (_preDataGridRow != null)
            SetDragOver(_preDataGridRow, false);
    }

    private void AssociatedObject_Drop(object sender, DragEventArgs e)
    {
        this.Clear();
        var point = e.GetPosition(this.AssociatedObject);
        DataGridRow dataGridRow = this.HitDataGridRow(point);
        if (dataGridRow == null)
            return;
        var dargGroup = e.Data.GetData("DragGroup");
        if (dargGroup is Adorner adorner && adorner.AdornedElement is DataGridRow row)
        {
            var sourceData = row.Item;
            var targetData = dataGridRow.Item;
            // 在数据集合中交换或移动项目
            var collection = AssociatedObject.ItemsSource as IList;
            if (collection != null)
            {
                int oldIndex = collection.IndexOf(sourceData);
                int newIndex = collection.IndexOf(targetData);

                collection.RemoveAt(oldIndex);
                collection.Insert(newIndex, sourceData);
            }
        }
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.Drop -= AssociatedObject_Drop;
        AssociatedObject.DragOver -= this.AssociatedObject_DragOver;
        AssociatedObject.GiveFeedback -= this.AssociatedObject_GiveFeedback;
    }
}
