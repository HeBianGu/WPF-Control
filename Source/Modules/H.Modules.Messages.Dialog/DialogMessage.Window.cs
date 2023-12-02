using H.Presenters.Common;
using H.Providers.Ioc;
using H.Windows.Dialog;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace H.Modules.Messages.Dialog
{
    public class WindowDialogMessageService : IDialogMessageService, IWindowDialogMessageService
    {
        public async Task<bool?> Show(object presenter, Action<IDialog> builder = null, Func<bool> canSumit = null)
        {
            bool? r = DialogWindow.ShowPresenter(presenter, builder, canSumit);
            return await Task.FromResult(r);
        }

        public async Task<T> ShowAction<P, T>(P presenter, Action<IDialog> builder = null, Func<IDialog, P, T> action = null)
        {
            T r = DialogWindow.ShowAction(presenter, action, builder);
            return await Task.FromResult(r);
        }

        public async Task<T> ShowPercent<T>(Func<IDialog, IPercentPresenter, T> action, Action<IDialog> build = null)
        {
            PercentPresenter p = new PercentPresenter();
            var r = DialogWindow.ShowAction(p, action, x =>
            {
                x.DialogButton = DialogButton.Cancel;
                build?.Invoke(x);
            });
            return await Task.FromResult(r);
        }

        public async Task<T> ShowString<T>(Func<IDialog, IStringPresenter, T> action, Action<IDialog> build = null)
        {
            StringPresenter p = new StringPresenter();
            T r = DialogWindow.ShowAction(p, action, x =>
            {
                x.HorizontalContentAlignment = HorizontalAlignment.Center;
                x.DialogButton = DialogButton.Cancel;
                build?.Invoke(x);
            });
            return await Task.FromResult(r);
        }

        public async Task<T> ShowWait<T>(Func<IDialog, T> action, Action<IDialog> build = null)
        {
            WaitPresenter p = new WaitPresenter();
            T r = DialogWindow.ShowAction(p, (d, p) => action.Invoke(d), x =>
            {
                x.DialogButton = DialogButton.Cancel;
                build?.Invoke(x);
            });
            return await Task.FromResult(r);
        }
    }
}
