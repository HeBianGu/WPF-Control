// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace H.Extensions.Behvaiors
{
    public class TextBoxUpdateSourceOnKeyDownBebavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.KeyDown += AssociatedObject_KeyDown;
        }


        public Key Key
        {
            get { return (Key)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty KeyProperty =
            DependencyProperty.Register("Key", typeof(Key), typeof(TextBoxUpdateSourceOnKeyDownBebavior), new FrameworkPropertyMetadata(Key.Return, (d, e) =>
            {
                TextBoxUpdateSourceOnKeyDownBebavior control = d as TextBoxUpdateSourceOnKeyDownBebavior;

                if (control == null) return;

                if (e.OldValue is Key o)
                {

                }

                if (e.NewValue is Key n)
                {

                }

            }));

        private void AssociatedObject_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == this.Key)
            {
                this.AssociatedObject.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.KeyDown -= AssociatedObject_KeyDown;


        }
    }


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
}
