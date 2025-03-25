using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace H.Extensions.Behvaiors.PasswordBoxs;

public class PasswordBindingBehavior : Behavior<PasswordBox>
{
    public string Password
    {
        get { return (string)GetValue(PasswordProperty); }
        set { SetValue(PasswordProperty, value); }
    }


    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.Register("Password", typeof(string), typeof(PasswordBindingBehavior), new FrameworkPropertyMetadata(default(string), (d, e) =>
        {
            PasswordBindingBehavior control = d as PasswordBindingBehavior;

            if (control == null)
                return;
            if (control.AssociatedObject == null)
                return;

            if (control.AssociatedObject.Password != e.NewValue?.ToString())
                control.AssociatedObject.Password = e.NewValue?.ToString();
        }));


    protected override void OnAttached()
    {
        this.AssociatedObject.Password = this.Password;
        this.AssociatedObject.PasswordChanged += AssociatedObject_PasswordChanged;
    }

    private void AssociatedObject_PasswordChanged(object sender, RoutedEventArgs e)
    {
        this.Password = this.AssociatedObject.Password;
    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.PasswordChanged -= AssociatedObject_PasswordChanged;
    }
}
