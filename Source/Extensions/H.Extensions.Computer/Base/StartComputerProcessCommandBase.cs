// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Commands;
using System.Diagnostics;

namespace H.Extensions.Computer.Base;


public abstract class StartComputerProcessCommandBase : DisplayMarkupCommandBase, IStartComputerProcessCommand
{
    public override void Execute(object parameter)
    {
        Process.Start(new ProcessStartInfo(this.GetFileName()) { UseShellExecute = true });
    }
    protected abstract string GetFileName();
}
