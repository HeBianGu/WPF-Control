// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace H.Themes.Default
{
    public abstract class WindowCommandBase : MarkupExtension, ICommand
    {
        public event EventHandler? CanExecuteChanged
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
        public bool CanExecute(object? parameter)
        {
            return parameter is Window;
        }

        public abstract void Execute(object? parameter);

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    public class CloseWindowCommand : WindowCommandBase
    {
        public override void Execute(object? parameter)
        {
            if (parameter is Window window)
                SystemCommands.CloseWindow(window);
        }
    }

    public class RestoreWindowCommand : WindowCommandBase
    {
        public override void Execute(object? parameter)
        {
            if (parameter is Window window)
                SystemCommands.RestoreWindow(window);
        }
    }

    public class MaximizeWindowCommand : WindowCommandBase
    {
        public override void Execute(object? parameter)
        {
            if (parameter is Window window)
                SystemCommands.MaximizeWindow(window);
        }
    }

    public class MinimizeWindowCommand : WindowCommandBase
    {
        public override void Execute(object? parameter)
        {
            if (parameter is Window window)
                SystemCommands.MinimizeWindow(window);
        }
    }

    public class SumitWindowCommand : WindowCommandBase
    {
        public override void Execute(object? parameter)
        {
            if (parameter is Window window)
            {
                window.DialogResult = true;
                SystemCommands.CloseWindow(window);
            }
        }
    }
}
