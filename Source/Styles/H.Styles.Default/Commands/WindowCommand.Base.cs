﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace H.Styles.Default
{
    public abstract class WindowCommandBase : MarkupExtension, ICommand
    {
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
        public bool CanExecute(object parameter)
        {
            return parameter is Window;
        }

        public abstract void Execute(object parameter);

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
