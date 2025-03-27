// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using Microsoft.Xaml.Behaviors;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace H.Extensions.Behvaiors.ItemsControls;

public abstract class ButtonBehaviorBase : Behavior<Button>
{
    protected override void OnAttached()
    {
        this.AssociatedObject.Click += AssociatedObject_Click;
    }

    private void AssociatedObject_Click(object sender, RoutedEventArgs e)
    {
        OnClick();
    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.Click -= AssociatedObject_Click;
    }

    protected abstract void OnClick();

    protected ItemsControl ItemsControl => this.AssociatedObject.GetParent<ItemsControl>();
    protected IList ItemsSource => this.ItemsControl?.ItemsSource as IList;
    protected object Item => this.AssociatedObject.DataContext;
}