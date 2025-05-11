// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Command;

[Icon("\xE77F")]
[Display(Name = "复制", Description = "复制文本到剪贴板")]
public class ClipBoardCopyTextCommand : DisplayMarkupCommandBase
{
    public override bool CanExecute(object parameter)
    {
        return parameter != null;
    }
    public override void Execute(object parameter)
    {
        if (parameter == null)
            return;
        try
        {
            Clipboard.SetText(parameter.ToString());
        }
        catch
        {

        }
    }
}
