using H.Controls.Form;
using H.Services.Common;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace H.Modules.Messages.Form
{
    public class FormMessageService : IFormMessageService
    {
        public async Task<bool?> ShowEdit<T>(T value, Predicate<T> match = null, Action<IDialog> action = null, Action<IFormOption> option = null, string title = null, Window owner = null)
        {
            Func<bool> canSumit = () =>
            {
                if (value.ModelStateDeep(out string error) == false)
                {
                    IocMessage.Dialog.Show(error);
                    return false;
                }
                return match?.Invoke(value) != false;
            };
            StaticFormPresenter presenter = new StaticFormPresenter(value);
            return await IocMessage.Dialog.Show(presenter, x =>
            {
                x.DialogButton = DialogButton.Sumit;
                x.Title = title;
                action?.Invoke(x);
            }, canSumit);
        }
        public async Task<bool?> ShowView<T>(T value, Predicate<T> match = null, Action<IDialog> action = null, Action<IFormOption> option = null, string title = null, Window owner = null)
        {
            Func<bool> canSumit = () =>
            {
                return match?.Invoke(value) != false;
            };
            StaticFormPresenter presenter = new StaticFormPresenter(value);
            presenter.UsePropertyView = true;
            return await IocMessage.Dialog.Show(presenter, x =>
            {
                x.DialogButton = DialogButton.Sumit;
                x.Title = title;
                action?.Invoke(x);
            }, canSumit);
        }
    }
}
