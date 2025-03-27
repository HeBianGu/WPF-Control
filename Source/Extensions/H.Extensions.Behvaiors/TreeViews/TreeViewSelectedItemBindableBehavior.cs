// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

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
        DependencyProperty.Register("SelectedItem", typeof(object), typeof(TreeViewSelectedItemBindableBehavior), new UIPropertyMetadata(null, OnSelectedItemChanged));

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


