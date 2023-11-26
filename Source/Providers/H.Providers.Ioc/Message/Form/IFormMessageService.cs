// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Threading.Tasks;
using System.Windows;

namespace H.Providers.Ioc
{
    public interface IFormMessageService
    {
        Task<bool?> ShowEdit<T>(T value, Predicate<T> match = null, Action<IDialogWindow> action = null, Action<IFormOption> option = null, string title = null, Window owner = null);
        Task<bool?> ShowView<T>(T value, Predicate<T> match = null, Action<IDialogWindow> action = null, Action<IFormOption> option = null, string title = null, Window owner = null);
    }

}
