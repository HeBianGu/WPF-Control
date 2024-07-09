// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Threading.Tasks;
using System.Windows;
using H.Providers.Ioc;

namespace System
{
    public static class IocMessage
    {
        public static IAdornerDialogMessageService Adorner => System.Ioc.GetService<IAdornerDialogMessageService>(throwIfNone: false);
        public static IWindowDialogMessageService Window => System.Ioc.GetService<IWindowDialogMessageService>(throwIfNone: false);
        public static IDialogMessageService Dialog => System.Ioc.GetService<IDialogMessageService>(throwIfNone: false);
        public static ISnackMessageService Snack => System.Ioc.GetService<ISnackMessageService>(throwIfNone: false);
        public static ITaskBarMessage TaskBar => System.Ioc.GetService<ITaskBarMessage>(throwIfNone: false);
        public static ISystemNotifyMessage SystemNotify => System.Ioc.GetService<ISystemNotifyMessage>(throwIfNone: false);
        public static INoticeMessageService Notify => System.Ioc.GetService<INoticeMessageService>(throwIfNone: false);
        public static IFormMessageService Form => System.Ioc.GetService<IFormMessageService>(throwIfNone: false);

        public static async Task<bool?> ShowDialogMessage(string message, string title = "提示", DialogButton dialogButton = DialogButton.Sumit)
        {
            if (Dialog == null)
            {
                MessageBoxButton boxButton = MessageBoxButton.OK;
                if (dialogButton == DialogButton.Sumit)
                    boxButton = MessageBoxButton.OK;
                if (dialogButton == DialogButton.Cancel)
                    boxButton = MessageBoxButton.OK;
                if (dialogButton == DialogButton.SumitAndCancel)
                    boxButton = MessageBoxButton.OKCancel;
                MessageBoxResult r = MessageBox.Show(message, title, boxButton);
                if (r == MessageBoxResult.None)
                    return new Nullable<bool>();
                return new Nullable<bool>(r == MessageBoxResult.OK);
            }
            else
            {
                return await Dialog.Show(message, x =>
                 {
                     x.DialogButton = dialogButton;
                     x.Title = title;
                 });
            }
        }

        public static async Task<bool?> ShowWindowMessage(string message, string title = "提示", DialogButton dialogButton = DialogButton.Sumit)
        {
            if (Window == null)
            {
                MessageBoxButton boxButton = MessageBoxButton.OK;
                if (dialogButton == DialogButton.Sumit)
                    boxButton = MessageBoxButton.OK;
                if (dialogButton == DialogButton.Cancel)
                    boxButton = MessageBoxButton.OK;
                if (dialogButton == DialogButton.SumitAndCancel)
                    boxButton = MessageBoxButton.OKCancel;
                MessageBoxResult r = MessageBox.Show(message, title, boxButton);
                if (r == MessageBoxResult.None)
                    return new Nullable<bool>();
                return new Nullable<bool>(r == MessageBoxResult.OK);
            }
            else
            {
                return await Window.Show(message, x =>
                {
                    x.DialogButton = dialogButton;
                    x.Title = title;
                });
            }
        }


        public static void ShowSnackMessage(string message)
        {
            if (Snack == null)
            {

            }
            else
            {
                Snack.ShowInfo(message);
            }
        }
    }
}
