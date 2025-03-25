// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace H.Extensions.Behvaiors.TreeViews;

public class TreeViewItemSetSelectedOnMouseDownBehavior : Behavior<FrameworkElement>
{
    public bool Use
    {
        get { return (bool)GetValue(UseProperty); }
        set { SetValue(UseProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty UseProperty =
        DependencyProperty.Register("Use", typeof(bool), typeof(TreeViewItemSetSelectedOnMouseDownBehavior), new FrameworkPropertyMetadata(default(bool), (d, e) =>
         {
             TreeViewItemSetSelectedOnMouseDownBehavior control = d as TreeViewItemSetSelectedOnMouseDownBehavior;

             if (control == null) return;

             if (e.OldValue is bool o)
             {

             }

             if (e.NewValue is bool n)
             {

             }

         }));

    protected override void OnAttached()
    {
        this.AssociatedObject.PreviewMouseDown += AssociatedObject_MouseDown; ;
    }

    private void AssociatedObject_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (!this.Use) return;
        var item = this.AssociatedObject.GetParent<TreeViewItem>();
        if (item == null) return;
        item.IsSelected = true;
    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.PreviewMouseDown -= AssociatedObject_MouseDown;
    }
}
