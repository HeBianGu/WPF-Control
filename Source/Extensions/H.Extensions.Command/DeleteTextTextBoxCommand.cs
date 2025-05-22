// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Command;

[Icon("\xE77F")]
[Display(Name = "删除", Description = "清空文本框文本")]
public class DeleteTextTextBoxCommand : DisplayMarkupCommandBase
{
    public override void Execute(object parameter)
    {
        if (parameter is TextBox textBox)
            textBox.Text = string.Empty;
    }

    public override bool CanExecute(object parameter)
    {
        if (parameter is TextBox textBox)
            return !string.IsNullOrEmpty(GetText(textBox));
        return false;
    }

    public static string GetText(DependencyObject obj)
    {
        return (string)obj.GetValue(TextProperty);
    }

    public static void SetText(DependencyObject obj, string value)
    {
        obj.SetValue(TextProperty, value);
    }

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.RegisterAttached("Text", typeof(string), typeof(DeleteTextTextBoxCommand), new PropertyMetadata(default(string), OnTextChanged));

    static public void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d as DependencyObject;

        string n = (string)e.NewValue;

        string o = (string)e.OldValue;
    }

}
