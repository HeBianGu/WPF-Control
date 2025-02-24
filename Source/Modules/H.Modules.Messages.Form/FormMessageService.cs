using H.Controls.Form;
using H.Services.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace H.Modules.Messages.Form
{
    public class FormMessageService : IFormMessageService
    {
        public async Task<bool?> ShowEdit<T>(T value, Action<IDialog> action = null, Predicate<T> match = null, Action<IFormOption> option = null, Window owner = null)
        {
            return await this.ShowEdit<T, StaticFormPresenter>(value, action, match, option, owner);
        }

        public async Task<bool?> ShowTabEdit<T>(T value, Action<IDialog> action = null, Predicate<T> match = null, Action<ITabFormOption> option = null, Window owner = null)
        {
            Action<ITabFormOption> toption = x =>
            {
                option?.Invoke(x);
                if (x.TabNames == null || x.TabNames.Count == 0)
                {
                    var ss = TypeDescriptor.GetProperties(value).OfType<PropertyDescriptor>().SelectMany(x => x.Attributes.OfType<DisplayAttribute>()).SelectMany(x => x.GroupName?.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)).Distinct();
                    x.TabNames = ss.ToObservable();
                }
            };

            return await this.ShowEdit<T, TabFormPresenter>(value, action, match, toption, owner);
        }

        public async Task<bool?> ShowView<T>(T value, Action<IDialog> action = null, Action<IFormOption> option = null, Window owner = null)
        {
            Action<IFormOption> optionView = x =>
            {
                option?.Invoke(x);
                x.UsePropertyView = true;
            };
            return await this.Show<T, StaticFormPresenter>(value, null, action, optionView, owner);
        }

        private async Task<bool?> ShowEdit<T, P>(T value, Action<IDialog> action = null, Predicate<T> match = null, Action<P> option = null, Window owner = null) where P : class, IFormOption, new()
        {
            Func<bool> canSumit = () =>
            {
                //如果传入验证方式则不用ModelState
                if (match != null)
                    return match.Invoke(value);

                if (value.ModelState(out List<string> errors) == false)
                {
                    IocMessage.Dialog.Show(errors.FirstOrDefault());
                    return false;
                }
                return true;
            };

            return await this.Show<T, P>(value, canSumit, action, option, owner);
        }

        private async Task<bool?> Show<T, P>(T value, Func<bool> canSumit = null, Action<IDialog> action = null, Action<P> option = null, Window owner = null) where P : class, IFormOption, new()
        {
            var presenter = Activator.CreateInstance<P>();
            presenter.SelectObject = value;
            option?.Invoke(presenter);
            return await IocMessage.Dialog.Show(presenter, x =>
            {
                x.DialogButton = DialogButton.Sumit;
                x.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                x.MinWidth = 400;
                action?.Invoke(x);
            }, canSumit);
        }

    }
}
