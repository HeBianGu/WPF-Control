// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace H.Extensions.Behvaiors.TreeViews;

public class TreeViewItemContainSelectItemBehavior : Behavior<TreeViewItem>
{
    public static bool GetIsContain(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsContainProperty);
    }

    public static void SetIsContain(DependencyObject obj, bool value)
    {
        obj.SetValue(IsContainProperty, value);
    }

    /// <summary> 应用窗体关闭和显示 </summary>
    public static readonly DependencyProperty IsContainProperty =
        DependencyProperty.RegisterAttached("IsContain", typeof(bool), typeof(TreeViewItemContainSelectItemBehavior), new PropertyMetadata(default(bool), OnIsContainChanged));

    static public void OnIsContainChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d as DependencyObject;

        bool n = (bool)e.NewValue;

        bool o = (bool)e.OldValue;
    }

    protected override void OnAttached()
    {
        this.AssociatedObject.Selected += AssociatedObject_Selected;
        this.AssociatedObject.Unselected += AssociatedObject_Unselected;
    }

    private void AssociatedObject_Unselected(object sender, RoutedEventArgs e)
    {
        Action<TreeViewItem> action = null;
        action = t =>
        {
            var parent = ItemsControl.ItemsControlFromItemContainer(t);
            if (parent is TreeViewItem item)
            {
                SetIsContain(item, false);
                action(item);
            }

            return;
        };
        action.Invoke(this.AssociatedObject);
    }

    private void AssociatedObject_Selected(object sender, RoutedEventArgs e)
    {
        Action<TreeViewItem> action = null;
        action = t =>
        {
            var parent = ItemsControl.ItemsControlFromItemContainer(t);
            if (parent is TreeViewItem item)
            {
                SetIsContain(item, true);
                action(item);
            }

            return;
        };
        action.Invoke(this.AssociatedObject);
    }
}
