// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")


namespace H.Styles.Controls;

public class TextBoxKeys
{
    public static ComponentResourceKey Default => new ComponentResourceKey(typeof(TextBoxKeys), "S.TextBox.Default");

    public static ComponentResourceKey Attach => new ComponentResourceKey(typeof(TextBoxKeys), "S.TextBox.Attach");

    public static ComponentResourceKey Delete => new ComponentResourceKey(typeof(TextBoxKeys), "S.TextBox.Delete");

    public static ComponentResourceKey Edit => new ComponentResourceKey(typeof(TextBoxKeys), "S.TextBox.Edit");

}

public class DeleteTextCommandExtension : MarkupExtension, ICommand
{
    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
        return parameter is TextBox text;
    }

    public void Execute(object parameter)
    {
        if (parameter is TextBox text)
            text.ClearValue(TextBox.TextProperty);
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
}

