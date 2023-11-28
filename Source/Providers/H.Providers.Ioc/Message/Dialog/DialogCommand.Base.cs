﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace H.Providers.Ioc
{
    public abstract class DialogCommandBase : IocMarkupCommandBase
    {
        protected virtual IDialog GetDialog(object parameter)
        {
            if (parameter is FrameworkElement element)
            {
                if (element is IDialog)
                    return element as IDialog;
                var parent= element.GetParent<FrameworkElement>(x => x?.DataContext is IDialog || x is IDialog);
                if (parent is IDialog dialog)
                    return dialog;
                else
                    return parent.DataContext as IDialog;

            }
            return null;
        }
    }
}