﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Threading.Tasks;

namespace H.Providers.Ioc
{
    public interface IDialogMessageService
    {
        Task<bool?> Show(object presenter, Action<IDialog> builder = null, Func<bool> canSumit = null);
        Task<T> ShowAction<P, T>(P presenter, Action<IDialog> builder = null, Func<IDialog, P, T> action = null);
        Task<T> ShowPercent<T>(Func<IDialog, IPercentPresenter, T> action, Action<IDialog> build = null);
        Task<T> ShowString<T>(Func<IDialog, IStringPresenter, T> action, Action<IDialog> build = null);
        Task<T> ShowWait<T>(Func<IDialog, T> action, Action<IDialog> build = null);
    }
}
