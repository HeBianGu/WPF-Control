// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Commands;
using System.Windows.Input;

namespace H.Windows.Main.Commands;

public abstract class WindowCommandBase : DisplayMarkupCommandBase, ICommand
{
    public override bool CanExecute(object parameter)
    {
        return parameter is Window && base.CanExecute(parameter);
    }

    public override Task ExecuteAsync(object parameter)
    {
        return Task.CompletedTask;
    }
}
