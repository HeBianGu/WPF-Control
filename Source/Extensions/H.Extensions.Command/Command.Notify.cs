// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Mvvm.ViewModels.Base;

namespace H.Extensions.Command;

public abstract class NotifyCommandBase : BindableBase, ICommand
{
    protected Action<object> _action;

    protected readonly Predicate<object> _canExecute;

    public NotifyCommandBase(Action<object> action)
    {
        _action = action;
    }

    public NotifyCommandBase(Action<object> execute, Predicate<object> canExecute)
    {
        _action = execute;

        _canExecute = canExecute ?? (x => true);
    }

    public virtual bool CanExecute(object parameter)
    {
        if (_canExecute == null) return true;

        return _canExecute(parameter);
    }

    public event EventHandler CanExecuteChanged
    {
        add
        {
            CommandManager.RequerySuggested += value;
        }
        remove
        {
            CommandManager.RequerySuggested -= value;
        }
    }

    public void Refresh()
    {
        CommandManager.InvalidateRequerySuggested();
    }

    public virtual void Execute(object parameter)
    {
        _action(parameter);
    }
}
