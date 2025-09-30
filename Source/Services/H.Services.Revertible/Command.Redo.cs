// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Commands;

namespace H.Services.Revertible;

public class RedoCommand : DisplayMarkupCommandBase
{
    public override void Execute(object parameter)
    {
        Ioc<IRevertibleService>.Instance?.Redo();
    }

    public override bool CanExecute(object parameter)
    {
        return Ioc<IRevertibleService>.Instance?.CanRedo == true;
    }
}
