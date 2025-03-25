// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace H.Extensions.Behvaiors.TextBoxs;

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

    private void AssociatedObject_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == this.Key)
            this.AssociatedObject.GetBindingExpression(TextBox.TextProperty).UpdateSource();
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        this.AssociatedObject.KeyDown -= AssociatedObject_KeyDown;


    }
}
