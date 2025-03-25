// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace H.Extensions.Behvaiors.TextBoxs;


public class TextBoxEditOnDoubleClickBebavior : Behavior<TextBox>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        this.AssociatedObject.Focusable = false;
        this.AssociatedObject.LostFocus += this.AssociatedObject_LostFocus;
        this.AssociatedObject.MouseDown += this.AssociatedObject_MouseDown;
        this.AssociatedObject.MouseLeave += this.AssociatedObject_MouseLeave;
    }

    private void AssociatedObject_MouseLeave(object sender, MouseEventArgs e)
    {
        this.AssociatedObject.Focusable = false;
    }

    private void AssociatedObject_MouseDown(object sender, MouseButtonEventArgs e)
    {
        this.AssociatedObject.Focusable = true;
    }

    private void AssociatedObject_LostFocus(object sender, RoutedEventArgs e)
    {
        this.AssociatedObject.Focusable = false;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        this.AssociatedObject.LostFocus -= this.AssociatedObject_LostFocus;
        this.AssociatedObject.MouseDown -= this.AssociatedObject_MouseDown;
        this.AssociatedObject.MouseLeave -= this.AssociatedObject_MouseLeave;
    }
}
