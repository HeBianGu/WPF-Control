// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Collections;

namespace H.Extensions.Behvaiors.ItemsControls;

public class ListBoxBindingSelectedItemsBehavior : Behavior<ListBox>
{

    public IList SelectedItems
    {
        get { return (IList)GetValue(SelectedItemsProperty); }
        set { SetValue(SelectedItemsProperty, value); }
    }

    public static readonly DependencyProperty SelectedItemsProperty =
        DependencyProperty.Register("SelectedItems", typeof(IList), typeof(ListBoxBindingSelectedItemsBehavior), new FrameworkPropertyMetadata(default(IList), (d, e) =>
        {
            ListBoxBindingSelectedItemsBehavior control = d as ListBoxBindingSelectedItemsBehavior;

            if (control == null)
                return;
            if (control.AssociatedObject == null)
                return;
            control.RefreshData();
        }));

    private void RefreshData()
    {
        if (this.AssociatedObject.SelectionMode == SelectionMode.Single)
            return;
        this.AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
        this.AssociatedObject.SelectedItems.Clear();
        foreach (object item in this.SelectedItems)
        {
            this.AssociatedObject.SelectedItems.Add(item);
        }
        this.AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
    }

    protected override void OnAttached()
    {
        RefreshData();
    }

    private void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        IList dataSource = this.SelectedItems;
        foreach (object item in e.AddedItems)
        {
            dataSource.Add(item);
        }
        foreach (object item in e.RemovedItems)
        {
            dataSource.Remove(item);
        }
    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
    }
}
