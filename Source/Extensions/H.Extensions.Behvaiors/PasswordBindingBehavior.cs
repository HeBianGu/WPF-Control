using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace H.Extensions.Behvaiors
{
    public class PasswordBindingBehavior : Behavior<PasswordBox>
    {
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(PasswordBindingBehavior), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                PasswordBindingBehavior control = d as PasswordBindingBehavior;

                if (control == null)
                    return;
                if (control.AssociatedObject == null)
                    return;
                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {
                    if (control.AssociatedObject.Password != n)
                        control.AssociatedObject.Password = n;
                }
            }));


        protected override void OnAttached()
        {
            this.AssociatedObject.Password = this.Password;
            this.AssociatedObject.PasswordChanged += AssociatedObject_PasswordChanged;
        }

        private void AssociatedObject_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Password = this.AssociatedObject.Password;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.PasswordChanged -= AssociatedObject_PasswordChanged;
        }
    }
}
