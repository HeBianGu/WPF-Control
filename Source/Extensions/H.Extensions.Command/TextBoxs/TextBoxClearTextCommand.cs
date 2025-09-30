// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Command.TextBoxs;

[Icon("\xE77F")]
[Display(Name = "删除", Description = "清空当前文本框文本")]
public class TextBoxClearTextCommand : DisplayMarkupCommandBase
{
    public override bool CanExecute(object parameter)
    {
        return parameter is TextBox tb && !string.IsNullOrEmpty(tb.Text);
    }
    public override void Execute(object parameter)
    {
        if (parameter is TextBox tb)
            tb.Text = null;
    }
}
