﻿
using H.Presenters.Common;
using H.Providers.Ioc;
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

namespace H.Modules.Messages.Dialog
{
    public class AdornerDialogMessageService : IDialogMessageService
    {
        public async Task<bool?> Show(object presenter, Action<IDialog> action = null, DialogButton dialogButton = DialogButton.Sumit, string title = null, Func<bool> canSumit = null, Window owner = null)
        {
            return await AdornerDialog.ShowPresenter(presenter, action, dialogButton, title, canSumit, owner);
        }

        public async Task<bool?> ShowIoc(Type type, Func<ICancelable, bool?> action = null, Action<IDialog> build = null, DialogButton dialogButton = DialogButton.Sumit, string title = null, Func<bool> canSumit = null, Window owner = null)
        {
            if (action == null)
            {
                var presenter = Ioc.Services.GetService(type);
                return await AdornerDialog.ShowIoc(type, build, title, dialogButton, owner);
            }
            else
            {
                var presenter = Ioc.Services.GetService(type);
                return await AdornerDialog.ShowAction(presenter, action, build, dialogButton, title, owner);  
            }
        }

        public async Task<bool?> ShowIoc<T>(Func<ICancelable, T, bool?> action = null, Action<IDialog> build = null, DialogButton dialogButton = DialogButton.Sumit, string title = null, Func<bool> canSumit = null, Window owner = null)
        {
            var presenter = (T)Ioc.Services.GetService(typeof(T));
            if (action == null)
            {
                return await AdornerDialog.ShowIoc<T>(build, title, dialogButton, owner);
            }
            else
            {
                return await AdornerDialog.ShowAction(presenter, x => action?.Invoke(x, presenter), build, dialogButton, title, owner);
            }
        }

        public async Task<bool?> ShowMessage(string message, string title = "提示", DialogButton dialogButton = DialogButton.Sumit, Action<IDialog> action = null, Window owner = null)
        {
            return await AdornerDialog.ShowMessage(message, title, dialogButton, owner, action);
        }

        public async Task<T> ShowPercent<T>(Func<IPercentPresenter, ICancelable, T> action, Action<IDialog> build = null, DialogButton dialogButton = DialogButton.Cancel, string title = null, Window owner = null)
        {
            var p = new PercentPresenter();
            return await AdornerDialog.ShowAction(p, cancel => action.Invoke(p, cancel), x =>
            {

            }, dialogButton, title, owner);
        }

        public async Task<T> ShowString<T>(Func<IStringPresenter, ICancelable, T> action, Action<IDialog> build = null, DialogButton dialogButton = DialogButton.Cancel, string title = null, Window owner = null)
        {
            var p = new StringPresenter();
            return await AdornerDialog.ShowAction(p, cancel => action.Invoke(p, cancel), x =>
            {
                x.HorizontalContentAlignment = HorizontalAlignment.Center;
            }, dialogButton, title, owner);
        }

        public async Task<T> ShowWait<T>(Func<ICancelable, T> action, Action<IDialog> build = null, DialogButton dialogButton = DialogButton.Cancel, string title = null, Window owner = null)
        {
            var p = new WaitPresenter();
            return await AdornerDialog.ShowAction(p, cancel => action.Invoke(cancel), x =>
            {

            }, dialogButton, title, owner);
        }
    }
}