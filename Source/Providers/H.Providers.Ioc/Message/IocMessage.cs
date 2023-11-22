// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using H.Providers.Ioc;
using System;
using System.Windows;

namespace H.Providers.Ioc
{
    public static class IocMessage
    {
        public static IDialogMessage Dialog => System.Ioc.GetService<IDialogMessage>(throwIfNone: false);
        public static ISnackMessage Snack => System.Ioc.GetService<ISnackMessage>(throwIfNone: false);
        public static ITaskBarMessage TaskBar => System.Ioc.GetService<ITaskBarMessage>(throwIfNone: false);
        public static ISystemNotifyMessage SystemNotify => System.Ioc.GetService<ISystemNotifyMessage>(throwIfNone: false);
        public static INotifyMessage Notify => System.Ioc.GetService<INotifyMessage>(throwIfNone: false);
        public static IFormMessage Form => System.Ioc.GetService<IFormMessage>(throwIfNone: false);

        //public static void ShowMessage(string message, string title = "提示")
        //{
        //    if (Dialog == null)
        //    {
        //        MessageBox.Show(message, title);
        //        return;
        //    }
        //    else
        //    {
        //        Dialog.ShowMessage(message, title);
        //    }
        //}

        public static bool? ShowMessage(string message, string title = "提示", DialogButton dialogButton = DialogButton.Sumit)
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
                return Dialog.ShowMessage(message, title, dialogButton).Result;
            }
        }


        public static void ShowSnackMessage(object message)
        {
            if (Snack == null)
            {

            }
            else
            {
                Snack.Show(message);
            }
        }
    }
}
