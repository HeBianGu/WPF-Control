// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Extensions.Common;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace H.Extensions.Behvaiors.TreeViews;

public class TreeViewSelectNoneOnMouseDownBehavior : Behavior<TreeView>
{
    protected override void OnAttached()
    {
        base.OnAttached();

        this.AssociatedObject.MouseDown += AssociatedObject_MouseDown;
    }

    private void AssociatedObject_MouseDown(object sender, MouseButtonEventArgs e)
    {
        Point point = Mouse.GetPosition(this.AssociatedObject);
        var item = this.AssociatedObject.HitTest<TreeViewItem>(point);
        if (item == null)
            this.AssociatedObject.SelectNone();

    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.MouseDown -= AssociatedObject_MouseDown;

    }
}


