// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Common
{
    public static class IocMessage
    {
        public static IAdornerDialogMessageService Adorner => Ioc.GetService<IAdornerDialogMessageService>(throwIfNone: false);
        public static IWindowDialogMessageService Window => Ioc.GetService<IWindowDialogMessageService>(throwIfNone: false);
        public static IDialogMessageService Dialog => Ioc.GetService<IDialogMessageService>(throwIfNone: false);
        public static ISnackMessageService Snack => Ioc.GetService<ISnackMessageService>(throwIfNone: false);
        public static ITaskBarMessage TaskBar => Ioc.GetService<ITaskBarMessage>(throwIfNone: false);
        public static ISystemNotifyMessage SystemNotify => Ioc.GetService<ISystemNotifyMessage>(throwIfNone: false);
        public static INoticeMessageService Notify => Ioc.GetService<INoticeMessageService>(throwIfNone: false);
        public static IFormMessageService Form => Ioc.GetService<IFormMessageService>(throwIfNone: false);

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
                return r == MessageBoxResult.None ? new Nullable<bool>() : new Nullable<bool>(r == MessageBoxResult.OK);
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
                return r == MessageBoxResult.None ? new Nullable<bool>() : new Nullable<bool>(r == MessageBoxResult.OK);
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
