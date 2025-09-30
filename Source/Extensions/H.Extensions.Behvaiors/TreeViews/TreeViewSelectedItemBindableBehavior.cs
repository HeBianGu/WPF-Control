// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Behvaiors.TreeViews;

public class TreeViewSelectedItemBindableBehavior : Behavior<TreeView>
{
    #region SelectedItem Property

    public object SelectedItem
    {
        get { return GetValue(SelectedItemProperty); }
        set { SetValue(SelectedItemProperty, value); }
    }

    public static readonly DependencyProperty SelectedItemProperty =
        DependencyProperty.Register("SelectedItem", typeof(object), typeof(TreeViewSelectedItemBindableBehavior), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedItemChanged));

    private static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        var item = e.NewValue as TreeViewItem;
        if (item != null)
            item.SetValue(TreeViewItem.IsSelectedProperty, true);
    }

    #endregion

    protected override void OnAttached()
    {
        base.OnAttached();
        this.AssociatedObject.SelectedItemChanged += OnTreeViewSelectedItemChanged;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();

        if (this.AssociatedObject != null)
            this.AssociatedObject.SelectedItemChanged -= OnTreeViewSelectedItemChanged;
    }

    private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        this.SelectedItem = e.NewValue;
    }
}

