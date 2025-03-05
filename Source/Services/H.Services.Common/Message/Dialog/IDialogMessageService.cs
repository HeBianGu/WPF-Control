// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

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

    public static class DialogMessageExtension
    {
        public static async Task<bool?> ShowDialog(this IDialogMessageService service, object presenter, Action<bool?> invokeAction, Action<IDialog> builder = null, Func<bool> canSumit = null)
        {
            var r = await service.Show(presenter, builder, canSumit);
            if (r != true)
                return r;
            invokeAction?.Invoke(r);
            return r;
        }

        public static async Task<bool?> ShowDeleteAllDialog(this IDialogMessageService service, Action<bool?> invokeAction)
        {
            return await service.ShowDialog("删除数据无法恢复，确定要全部删除？", invokeAction);

        }
        public static async Task<bool?> ShowDeleteDialog(this IDialogMessageService service, Action<bool?> invokeAction)
        {
            return await service.ShowDialog("删除数据无法恢复，确定要删除？", invokeAction);
        }

        public static async Task<bool?> ShowNotImplementedDialog(this IDialogMessageService service)
        {
            return await service.Show("没有实现该功能");
        }
    }
}
