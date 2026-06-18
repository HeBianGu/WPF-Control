// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Computer.Base;

namespace H.Extensions.Computer.ComputerProcesses;

public class ComputerProcessInfoPresenter : StartComputerProcessCommandsPresenterBase
{
    public ComputerProcessInfoPresenter()
    {

    }

    protected override IEnumerable<IStartComputerProcessCommand> GetStartComputerProcessCommands()
    {
        return (IEnumerable<IStartComputerProcessCommand>)this.GetType().Assembly.GetTypes()
           .Where(x => typeof(IShowComputerProcessCommand).IsAssignableFrom(x) && !x.IsAbstract)
           .Select(x => (IShowComputerProcessCommand)Activator.CreateInstance(x));
    }
}
