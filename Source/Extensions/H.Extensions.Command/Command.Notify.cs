// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Mvvm.ViewModels.Base;
using System.Windows.Input;

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
