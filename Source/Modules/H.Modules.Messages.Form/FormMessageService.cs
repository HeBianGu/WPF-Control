using H.Controls.Form;
using H.Providers.Ioc;
using H.Windows.Dialog;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace H.Modules.Messages.Form
{
    public class FormMessageService : IFormMessageService
    {
        public async Task<bool?> ShowEdit<T>(T value, Predicate<T> match = null, Action<IDialogWindow> action = null, Action<IFormOption> option = null, string title = null, Window owner = null)
        {
            Func<bool> canSumit = () =>
            {
                if (value.ModelStateDeep(out string error) == false)
                {
                    IocMessage.Dialog.ShowMessage(error);
                    return false;
                }
                return match?.Invoke(value) != false;
            };
            var presenter = new StaticFormPresenter(value);
            return await IocMessage.Dialog.Show(presenter, action, DialogButton.Sumit, title, canSumit, owner);
        }
        public async Task<bool?> ShowView<T>(T value, Predicate<T> match = null, Action<IDialogWindow> action = null, Action<IFormOption> option = null, string title = null, Window owner = null)
        {
            Func<bool> canSumit = () =>
            {
                return match?.Invoke(value) != false;
            };
            var presenter = new StaticFormPresenter(value);
            presenter.UsePropertyView = true;
            return await IocMessage.Dialog.Show(presenter, action, DialogButton.Sumit, title, canSumit, owner);
        }
    }
}
