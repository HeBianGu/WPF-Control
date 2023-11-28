using H.Controls.Adorner;
using H.Presenters.Common;
using H.Providers.Ioc;
using H.Providers.Ioc;
using H.Windows.Dialog;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace H.Modules.Messages.Dialog
{
    public static class AdornerDialog
    {
        public static async Task<bool?> ShowMessage(string message, string title = "提示", DialogButton dialogButton = DialogButton.Sumit, Window owner = null, Action<IDialog> action = null)
        {
            Action<IDialog> build = x =>
            {
                if (x is AdornerDialogPresenter c)
                {
                    c.MinWidth = 400;
                    c.MinHeight = 200;
                    c.MinHeight = 150;
                    c.HorizontalContentAlignment = HorizontalAlignment.Center;
                    c.RefreshButton(dialogButton);

                }
                action?.Invoke(x);
            };

            return await ShowPresenter(new MessagePresenter() { Value = message }, build, DialogButton.Sumit, title, null, owner);
        }

        public static async Task<bool?> ShowPresenter(object presenter, Action<AdornerDialogPresenter> action = null, DialogButton dialogButton = DialogButton.Sumit, string title = null, Func<bool> canSumit = null, Window owner = null)
        {
            var dialog = new AdornerDialogPresenter(presenter);
            dialog.Title = title ?? presenter.GetType().GetCustomAttribute<DisplayAttribute>()?.Name ?? "提示";
            dialog.MinWidth = 500;
            dialog.CanSumit = canSumit;
            action?.Invoke(dialog);
            dialog.RefreshButton(dialogButton);
            return await dialog.ShowDialog();
        }

        public static async Task<T> ShowAction<T>(object data, Func<ICancelable, T> func, Action<AdornerDialogPresenter> action = null, DialogButton dialogButton = DialogButton.Cancel, string title = null, Window owner = null)
        {
            AdornerDialogPresenter dialog = new AdornerDialogPresenter(data);
            dialog.Title = title ?? data.GetType().GetCustomAttribute<DisplayAttribute>()?.Name ?? "提示";
            dialog.MinWidth = 500;
            dialog.MinHeight = 150;
            dialog.RefreshButton(dialogButton);
            T result = default;
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
            Task.Run(() =>
            {
                result = func.Invoke(dialog);
                //dialog.Dispatcher.Invoke(() =>
                //{
                //    if (dialog.DialogResult == null)
                //        dialog.DialogResult = true;
                //});
                dialog.DialogResult = true;
                dialog.Close();
            });
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
            await dialog.ShowDialog();
            return result;
        }

        public static async Task<bool?> ShowPresenter(object data, string title)
        {
            return await ShowPresenter(data, null, DialogButton.Sumit, title);
        }

        public static async Task<bool?> ShowIoc<T>(Action<AdornerDialogPresenter> action = null, string title = null, DialogButton dialogButton = DialogButton.Sumit, Window owner = null)
        {
            return await ShowIoc(typeof(T), action, title, dialogButton, owner);
        }

        public static async Task<bool?> ShowIoc(Type type, Action<AdornerDialogPresenter> action = null, string title = null, DialogButton dialogButton = DialogButton.Sumit, Window owner = null)
        {
            var presenter = Ioc.Services.GetService(type);
            return await ShowPresenter(presenter, x =>
            {
                x.MinWidth = 500;
                x.MinWidth = 300;
                action?.Invoke(x);
            }, dialogButton, title, null, owner);
        }
    }
}
