// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Mvvm;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace H.Styles.Default
{
    public abstract class WindowCommandBase : DisplayMarkupCommandBase, ICommand
    {
        public override bool CanExecute(object parameter)
        {
            return parameter is Window && base.CanExecute(parameter);
        }

        public override Task ExecuteAsync(object parameter)
        {
            return Task.CompletedTask;
        }
    }
}
