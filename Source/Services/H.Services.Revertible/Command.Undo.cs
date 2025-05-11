// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Common.Commands;
using System.ComponentModel.DataAnnotations;

namespace H.Services.Revertible;

[Icon("\xE7A7")]
[Display(Name = "撤销", Description = "撤销当前操作")]
public class UndoCommand : DisplayMarkupCommandBase
{
    public override void Execute(object parameter)
    {
        Ioc<IRevertibleService>.Instance?.Undo();
    }
    public override bool CanExecute(object parameter)
    {
        return Ioc<IRevertibleService>.Instance?.CanUndo == true;
    }
}
