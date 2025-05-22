// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Behvaiors.TextBoxs;

public class TextBoxSelectAllOnFocusBebavior : Behavior<TextBox>
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
