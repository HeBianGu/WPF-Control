﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;

namespace H.Extensions.Behvaiors.ContextMenus;

public class ContextMenuDisplayBehavior : Behavior<FrameworkElement>
{

    protected override void OnAttached()
    {
        base.OnAttached();

        this.AssociatedObject.MouseDown += AssociatedObject_MouseDown;
        this.AssociatedObject.MouseUp += AssociatedObject_MouseUp;
    }


    public MouseButton MouseButton
    {
        get { return (MouseButton)GetValue(MouseButtonProperty); }
        set { SetValue(MouseButtonProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MouseButtonProperty =
        DependencyProperty.Register("MouseButton", typeof(MouseButton), typeof(ContextMenuDisplayBehavior), new PropertyMetadata(MouseButton.Right, (d, e) =>
         {
             ContextMenuDisplayBehavior control = d as ContextMenuDisplayBehavior;

             if (control == null) return;

             //MouseButton config = e.NewValue as MouseButton;

         }));
    private Point _startPos;

    private void AssociatedObject_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (this.AssociatedObject.ContextMenu == null) return;

        e.Handled = true;

        this.AssociatedObject.ContextMenu.IsOpen = false;

        if (this.MouseButton != e.ChangedButton) return;

        Point point = e.GetPosition(this.AssociatedObject);

        Point currentPosition = e.GetPosition(this.AssociatedObject);

        if (Math.Abs(currentPosition.X - _startPos.X) > 5 ||
                 Math.Abs(currentPosition.Y - _startPos.Y) > 5)
            return;
        this.AssociatedObject.ContextMenu.PlacementTarget = this.AssociatedObject;
        this.AssociatedObject.ContextMenu.IsOpen = true;


    }

    private void AssociatedObject_MouseDown(object sender, MouseButtonEventArgs e)
    {
        _startPos = e.GetPosition(this.AssociatedObject);
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();

        this.AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
        this.AssociatedObject.MouseUp -= AssociatedObject_MouseUp;
    }
}
