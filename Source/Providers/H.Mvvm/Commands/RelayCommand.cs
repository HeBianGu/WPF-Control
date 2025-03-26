// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Mvvm.Commands.Base;

namespace H.Mvvm.Commands;

public class RelayCommand : CommandBase
{
    protected Action<object> _action;
    protected readonly Predicate<object> _canExecute;

    public RelayCommand(Action<object> action)
    {
        _action = action;
    }
    public RelayCommand(Action<object> execute, Predicate<object> canExecute) : this(execute)
    {
        _canExecute = canExecute;
    }

    public override void Execute(object parameter)
    {
        if (_action != null)
            _action(parameter);
    }

    public override bool CanExecute(object parameter)
    {
        return _canExecute == null ? true : _canExecute.Invoke(parameter);
    }
}
