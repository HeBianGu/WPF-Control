// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Behvaiors.TextBoxs;

[Obsolete("未测试需要测试是否有效")]
public class TextBoxUpdateSourceOnKeyDownBebavior : Behavior<TextBox>
{
    protected override void OnAttached()
    {
        base.OnAttached();

        //EventManager.RegisterClassHandler(
        //  typeof(TextBox),
        //  UIElement.GotKeyboardFocusEvent,
        //  new RoutedEventHandler(SelectAllText),
        //  true);

        //EventManager.RegisterClassHandler(
        //    typeof(TextBox),
        //    UIElement.PreviewMouseLeftButtonDownEvent,
        //    new MouseButtonEventHandler(IgnoreMouseClick),
        //    true);

        this.AssociatedObject.GotKeyboardFocus += SelectAllText;
        this.AssociatedObject.PreviewMouseLeftButtonDown += IgnoreMouseClick;
    }

    private void IgnoreMouseClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is TextBox textBox && !textBox.IsKeyboardFocusWithin)
        {
            e.Handled = true;
            textBox.Focus();
        }
    }

    private void SelectAllText(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox textBox && !textBox.IsReadOnly)
        {
            textBox.Dispatcher.BeginInvoke(new Action(() =>
            {
                textBox.SelectAll();
            }), System.Windows.Threading.DispatcherPriority.Input);
        }
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();

        this.AssociatedObject.GotKeyboardFocus -= SelectAllText;
        this.AssociatedObject.PreviewMouseLeftButtonDown -= IgnoreMouseClick;
    }
}
