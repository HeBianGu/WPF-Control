// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Mvvm
{
    public class InvokeCommand : CommandBase
    {
        protected Action<object> _action;
        protected readonly Predicate<object> _canExecute;

        public InvokeCommand(Action<object> action)
        {
            _action = action;
        }
        public InvokeCommand(Action<object> execute, Predicate<object> canExecute) : this(execute)
        {
            _canExecute = canExecute ?? (x => true);
        }

        public override void Execute(object parameter)
        {
            if (_action != null)
                _action(parameter);
        }

        public override bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                return _canExecute(parameter);
            return true;
        }
    }
}
