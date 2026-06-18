// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.ViewModels.Base;
using System.Collections.ObjectModel;

namespace H.Extensions.Computer.Base;

public abstract class StartComputerProcessCommandsPresenterBase : DisplayBindableBase
{
    public StartComputerProcessCommandsPresenterBase()
    {
        this.StartComputerProcessCommands = new ObservableCollection<IStartComputerProcessCommand>(this.GetStartComputerProcessCommands());
    }
    private ObservableCollection<IStartComputerProcessCommand> _StartComputerProcessCommands = new ObservableCollection<IStartComputerProcessCommand>();
    public ObservableCollection<IStartComputerProcessCommand> StartComputerProcessCommands
    {
        get { return _StartComputerProcessCommands; }
        set
        {
            _StartComputerProcessCommands = value;
            RaisePropertyChanged();
        }
    }

    protected abstract IEnumerable<IStartComputerProcessCommand> GetStartComputerProcessCommands();
}
