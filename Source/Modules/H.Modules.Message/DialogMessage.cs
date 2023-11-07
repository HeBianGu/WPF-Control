using H.Presenters.Common;
using H.Providers.Ioc;
using H.Windows.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace H.Modules.Message
{
    public class DialogMessage : IDialogMessage
    {
        public async Task<bool?> Show(object presenter, Action<Control> action = null, DialogButton dialogButton = DialogButton.Sumit, string title = null, Func<bool> canSumit = null, Window owner = null)
        {
            var r = DialogWindow.ShowPresenter(presenter, action, dialogButton, title, canSumit, owner);
            return await Task.FromResult(r);
        }

        public async Task<bool?> ShowIoc(Type type, Func<ICancelable, bool?> action = null, Action<Control> build = null, string title = null, Func<bool> canSumit = null, Window owner = null)
        {
            if (action == null)
            {
                var r = DialogWindow.ShowIoc(type, build, title, owner);
                return await Task.FromResult(r);
            }
            else
            {
                var instance = Ioc.Services.GetService(type);
                var r = DialogWindow.ShowAction(instance, action, build, title, owner);
                return await Task.FromResult(r);
            }
        }

        public async Task<bool?> ShowIoc<T>(Func<ICancelable, T, bool?> action = null, Action<Control> build = null, string title = null, Func<bool> canSumit = null, Window owner = null)
        {
            if (action == null)
            {
                var r = DialogWindow.ShowIoc<T>(build, title, owner);
                return await Task.FromResult(r);
            }
            else
            {
                var instance = (T)Ioc.Services.GetService(typeof(T));
                var r = DialogWindow.ShowAction<bool?>(instance, x => action?.Invoke(x, instance), build, title, owner);
                return await Task.FromResult(r);
            }

        }

        public async Task<bool?> ShowMessage(string message, string title = "提示", DialogButton dialogButton = DialogButton.Sumit, Action<Control> action = null, Window owner = null)
        {
            var r = DialogWindow.ShowMessage(message, title, dialogButton, owner, action);
            return await Task.FromResult(r);
        }

        public async Task<T> ShowPercent<T>(Func<IPercentPresenter, ICancelable, T> action, Action<Control> build = null, string title = null, Window owner = null)
        {
            var p = new PercentPresenter();
            var r = DialogWindow.ShowAction(p, cancel => action.Invoke(p, cancel), x =>
            {

            }, title, owner);
            return await Task.FromResult(r);
        }

        public async Task<T> ShowString<T>(Func<IStringPresenter, ICancelable, T> action, Action<Control> build = null, string title = null, Window owner = null)
        {
            var p = new StringPresenter();
            var r = DialogWindow.ShowAction(p, cancel => action.Invoke(p, cancel), x =>
            {
                x.HorizontalContentAlignment = HorizontalAlignment.Center;
            }, title, owner);
            return await Task.FromResult(r);
        }

        public async Task<T> ShowWait<T>(Func<ICancelable, T> action, Action<Control> build = null, string title = null, Window owner = null)
        {
            var p = new WaitPresenter();
            var r = DialogWindow.ShowAction(p, cancel => action.Invoke(cancel), x =>
            {

            }, title, owner);
            return await Task.FromResult(r);
        }
    }
}
