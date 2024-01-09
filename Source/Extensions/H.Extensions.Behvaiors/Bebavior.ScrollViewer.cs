// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace H.Extensions.Behvaiors
{
    public class ScrollViewerBebavior : Behavior<ScrollViewer>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.PreviewMouseWheel += AssociatedObject_PreviewMouseWheel;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.PreviewMouseWheel -= AssociatedObject_PreviewMouseWheel;

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
                {
                    this.AssociatedObject.LineRight();
                }
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
                    if (this.AssociatedObject.ViewportHeight + this.AssociatedObject.VerticalOffset >= this.AssociatedObject.ExtentHeight && e.Delta <= 0)
                        e.Handled = true;
                    if (this.AssociatedObject.VerticalOffset == 0 && e.Delta > 0)
                        e.Handled = true;
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
}
