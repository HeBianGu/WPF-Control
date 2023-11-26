// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Threading.Tasks;

namespace H.Providers.Ioc
{
    public interface ISnackMessageService
    {
        //void Show(object message);
        //void Show(object message, object actionContent, Action actionHandler);
        //void Show<TArgument>(string message, object actionContent, Action<TArgument> actionHandler, TArgument actionArgument);
        //void ShowTime(object message);

        Task<bool?> ShowDialog(string message);
        void ShowError(string message);
        void ShowFatal(string message);
        void ShowInfo(string message);
        void Show(ISnackItem message);
        Task<T> ShowProgress<T>(Func<IPercentSnackItem, T> action);
        Task<T> ShowString<T>(Func<ISnackItem, T> action);
        void ShowSuccess(string message);
        void ShowWarn(string message);
    }
}
