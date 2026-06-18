// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Computer.Base;

namespace H.Extensions.Computer.ComputerProcesses;

public abstract class ShowComputerProcessCommandBase : StartComputerProcessCommandBase, IShowComputerProcessCommand
{
    public ShowComputerProcessCommandBase()
    {
        ComputerProcessInfoAttribute processInfoAttribute = this.GetType().GetCustomAttributes(typeof(ComputerProcessInfoAttribute), false)
            .OfType<ComputerProcessInfoAttribute>()
            .FirstOrDefault();
        if (processInfoAttribute != null)
            this.FileName = processInfoAttribute.FileName;
    }

    public string FileName { get; set; }

    protected override string GetFileName()
    {
        return this.FileName;
    }
}
