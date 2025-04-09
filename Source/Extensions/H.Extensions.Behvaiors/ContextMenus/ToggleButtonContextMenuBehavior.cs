// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace H.Extensions.Behvaiors.ContextMenus;

public class ToggleButtonContextMenuBehavior : Behavior<ToggleButton>
{
    protected override void OnAttached()
    {
        this.AssociatedObject.Loaded += AssociatedObject_Loaded;
    }

    private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
    {
        this.AssociatedObject.Checked += AssociatedObject_Checked;
        this.AssociatedObject.Unchecked += AssociatedObject_Unchecked;
        this.AssociatedObject.ContextMenu.Closed += ContextMenu_Closed;
    }

    private void AssociatedObject_ContextMenuClosing(object sender, System.Windows.Controls.ContextMenuEventArgs e)
    {
        this.AssociatedObject.IsChecked = false;
    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.Loaded -= AssociatedObject_Loaded;
        this.AssociatedObject.Checked -= AssociatedObject_Checked;
        this.AssociatedObject.Unchecked -= AssociatedObject_Unchecked;
        this.AssociatedObject.ContextMenu.Closed -= ContextMenu_Closed;
    }

    private void ContextMenu_Closed(object sender, EventArgs e)
    {
        this.AssociatedObject.IsChecked = false;
    }

    private void AssociatedObject_Unchecked(object sender, RoutedEventArgs e)
    {
        this.AssociatedObject.ContextMenu.IsOpen = false;
    }

    private void AssociatedObject_Checked(object sender, RoutedEventArgs e)
    {
        this.AssociatedObject.ContextMenu.PlacementTarget = this.AssociatedObject;
        this.AssociatedObject.ContextMenu.IsOpen = true;
    }
}
