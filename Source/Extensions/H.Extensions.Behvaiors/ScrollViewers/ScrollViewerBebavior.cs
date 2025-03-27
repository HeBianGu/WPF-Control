// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Extensions.Common;
global using Microsoft.Xaml.Behaviors;
global using System.Windows;
global using System.Windows.Controls;

namespace H.Extensions.Behvaiors.ScrollViewers;

public class ScrollViewerBebavior : Behavior<ScrollViewer>
{
    protected override void OnAttached()
    {
        base.OnAttached();

        this.AssociatedObject.PreviewMouseWheel += AssociatedObject_PreviewMouseWheel;
        this.AssociatedObject.MouseWheel += AssociatedObject_MouseWheel;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        this.AssociatedObject.PreviewMouseWheel -= AssociatedObject_PreviewMouseWheel;
        this.AssociatedObject.MouseWheel -= AssociatedObject_MouseWheel;
    }

    private void AssociatedObject_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
    {
        if (this.UseMouseWheelHijack)
        {
            if (this.AssociatedObject.ViewportHeight + this.AssociatedObject.VerticalOffset >= this.AssociatedObject.ExtentHeight && e.Delta <= 0)
                e.Handled = true;
            if (this.AssociatedObject.VerticalOffset == 0 && e.Delta > 0)
                e.Handled = true;
        }
    }

    private void AssociatedObject_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
    {
        if (e.Handled)
            return;

        if (this.UseHorizontalMouseWheel)
        {
            if (this.UseMouseWheelHijack)
            {
                if (this.AssociatedObject.ViewportWidth + this.AssociatedObject.HorizontalOffset >= this.AssociatedObject.ExtentWidth && e.Delta <= 0)
                    return;
                if (this.AssociatedObject.HorizontalOffset == 0 && e.Delta > 0)
                    return;
            }

            if (e.Handled)
                return;
            if (e.Delta < 0)
                this.AssociatedObject.LineRight();
            else
            {
                this.AssociatedObject.LineLeft();

            }
            e.Handled = true;
        }
        else
        {

            if (this.UseMouseWheelHijack)
            {
                var pSv = this.AssociatedObject.GetParent<ScrollViewer>();
                if (pSv != null)
                {
                    if (this.AssociatedObject.ViewportHeight + this.AssociatedObject.VerticalOffset >= this.AssociatedObject.ExtentHeight && e.Delta <= 0)
                        pSv.LineDown();
                    if (this.AssociatedObject.VerticalOffset == 0 && e.Delta > 0)
                        pSv.LineUp();
                }
            }
        }
    }

    public bool UseHorizontalMouseWheel
    {
        get { return (bool)GetValue(UseHorizontalMouseWheelProperty); }
        set { SetValue(UseHorizontalMouseWheelProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty UseHorizontalMouseWheelProperty =
        DependencyProperty.Register("UseHorizontalMouseWheel", typeof(bool), typeof(ScrollViewerBebavior), new FrameworkPropertyMetadata(default(bool), (d, e) =>
        {
            ScrollViewerBebavior control = d as ScrollViewerBebavior;

            if (control == null) return;

            if (e.OldValue is bool o)
            {

            }

            if (e.NewValue is bool n)
            {

            }

        }));


    public bool UseMouseWheelHijack
    {
        get { return (bool)GetValue(UseMouseWheelHijackProperty); }
        set { SetValue(UseMouseWheelHijackProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty UseMouseWheelHijackProperty =
        DependencyProperty.Register("UseMouseWheelHijack", typeof(bool), typeof(ScrollViewerBebavior), new FrameworkPropertyMetadata(default(bool), (d, e) =>
        {
            ScrollViewerBebavior control = d as ScrollViewerBebavior;

            if (control == null) return;

            if (e.OldValue is bool o)
            {

            }

            if (e.NewValue is bool n)
            {

            }

        }));

}
