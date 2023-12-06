
using H.Presenters.Common;
using H.Providers.Ioc;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace H.Modules.Messages.Dialog
{
    public class AdornerDialogMessageService : IDialogMessageService, IAdornerDialogMessageService
    {
        public async Task<bool?> Show(object presenter, Action<IDialog> builder = null, Func<bool> canSumit = null)
        {
            var data = presenter is string str ? new StringPresenter() { Value = str } : presenter;
            return await AdornerDialog.ShowPresenter(data, builder, canSumit);
        }

        public async Task<T> ShowAction<P, T>(P presenter, Action<IDialog> builder = null, Func<IDialog, P, T> action = null)
        {
            return await AdornerDialog.ShowAction(presenter, action, builder);
        }

        public async Task<T> ShowPercent<T>(Func<IDialog, IPercentPresenter, T> action, Action<IDialog> build = null)
        {
            PercentPresenter p = new PercentPresenter();
            return await AdornerDialog.ShowAction(p, action, x =>
            {
                x.DialogButton = DialogButton.Cancel;
                build?.Invoke(x);
            });
        }

        public async Task<T> ShowString<T>(Func<IDialog, IStringPresenter, T> action, Action<IDialog> build = null)
        {
            StringPresenter p = new StringPresenter();
            return await AdornerDialog.ShowAction(p, action, x =>
            {
                x.DialogButton = DialogButton.Cancel;
                x.HorizontalContentAlignment = HorizontalAlignment.Center;
                build?.Invoke(x);
            });
        }

        public async Task<T> ShowWait<T>(Func<IDialog, T> action, Action<IDialog> build = null)
        {
            return await AdornerDialog.ShowAction(new WaitPresenter(), (d, p) => action.Invoke(d), x =>
            {
                x.DialogButton = DialogButton.Cancel;
                build?.Invoke(x);
            });
        }
    }
}
