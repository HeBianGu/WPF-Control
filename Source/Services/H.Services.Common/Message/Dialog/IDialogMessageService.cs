// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Common
{
    public interface IDialogMessageService
    {
        Task<bool?> Show(object presenter, Action<IDialog> builder = null, Func<bool> canSumit = null);
        Task<T> ShowAction<P, T>(P presenter, Action<IDialog> builder = null, Func<IDialog, P, T> action = null);
        Task<T> ShowPercent<T>(Func<IDialog, IPercentPresenter, T> action, Action<IDialog> build = null);
        Task<T> ShowString<T>(Func<IDialog, IStringPresenter, T> action, Action<IDialog> build = null);
        Task<T> ShowWait<T>(Func<IDialog, T> action, Action<IDialog> build = null);

        Task<bool> ShowForeach<T>(Func<IEnumerable<T>> getList, Func<T, Tuple<bool, string>> itemAction, Action<IDialog> build = null);

    }
}
