// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Commands;
using System.Windows;

namespace H.Windows.Dialog;

public class SumitWindowCommand : DisplayMarkupCommandBase
{
    public override void Execute(object parameter)
    {
        if (parameter is Window window)
        {
            window.DialogResult = true;
            SystemCommands.CloseWindow(window);
        }
    }
}
