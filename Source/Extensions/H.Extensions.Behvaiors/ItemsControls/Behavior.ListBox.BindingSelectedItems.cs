// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Threading;
using System.Collections;

namespace H.Extensions.Behvaiors.ItemsControls;

public class ListBoxBindingSelectedItemsBehavior : Behavior<ListBox>
{
    private static readonly DependencyPropertyDescriptor ItemsSourceDescriptor =
        DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(ListBox));

    private INotifyCollectionChanged _itemsSourceCollection;
    private bool _refreshPending;

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
        ListBox listBox = this.AssociatedObject;
        if (listBox == null || listBox.SelectionMode == SelectionMode.Single)
            return;

        listBox.SelectionChanged -= AssociatedObject_SelectionChanged;
        try
        {
            IList selectedItems = this.SelectedItems;
            if (selectedItems != null)
            {
                for (int i = selectedItems.Count - 1; i >= 0; i--)
                {
                    if (!listBox.Items.Contains(selectedItems[i]))
                        selectedItems.RemoveAt(i);
                }
            }

            listBox.SelectedItems.Clear();
            if (selectedItems != null)
            {
                foreach (object item in selectedItems)
                    listBox.SelectedItems.Add(item);
            }
        }
        finally
        {
            listBox.SelectionChanged += AssociatedObject_SelectionChanged;
        }
    }

    private void OnItemsSourceChanged(object sender, EventArgs e)
    {
        this.SubscribeItemsSource();
        this.QueueRefreshData();
    }

    private void SubscribeItemsSource()
    {
        if (_itemsSourceCollection != null)
            _itemsSourceCollection.CollectionChanged -= ItemsSourceCollection_CollectionChanged;

        _itemsSourceCollection = this.AssociatedObject?.ItemsSource as INotifyCollectionChanged;
        if (_itemsSourceCollection != null)
            _itemsSourceCollection.CollectionChanged += ItemsSourceCollection_CollectionChanged;
    }

    private void ItemsSourceCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        this.QueueRefreshData();
    }

    private void QueueRefreshData()
    {
        ListBox listBox = this.AssociatedObject;
        if (listBox == null || _refreshPending)
            return;

        _refreshPending = true;
        listBox.Dispatcher.BeginInvoke(DispatcherPriority.DataBind, new Action(() =>
        {
            _refreshPending = false;
            if (ReferenceEquals(this.AssociatedObject, listBox))
                this.RefreshData();
        }));
    }

    protected override void OnAttached()
    {
        this.AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
        ItemsSourceDescriptor.AddValueChanged(this.AssociatedObject, OnItemsSourceChanged);
        this.SubscribeItemsSource();
        RefreshData();
    }

    private void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        IList dataSource = this.SelectedItems;
        if (dataSource == null)
            return;

        foreach (object item in e.AddedItems)
        {
            if (!dataSource.Contains(item))
                dataSource.Add(item);
        }
        foreach (object item in e.RemovedItems)
        {
            if (this.AssociatedObject.Items.Contains(item))
                dataSource.Remove(item);
        }
    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
        ItemsSourceDescriptor.RemoveValueChanged(this.AssociatedObject, OnItemsSourceChanged);
        if (_itemsSourceCollection != null)
        {
            _itemsSourceCollection.CollectionChanged -= ItemsSourceCollection_CollectionChanged;
            _itemsSourceCollection = null;
        }
        _refreshPending = false;
    }
}
