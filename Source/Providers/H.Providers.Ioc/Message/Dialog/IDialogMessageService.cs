// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Reflection.Metadata;
using H.Providers.Ioc;

namespace H.Providers.Ioc
{
    public interface IDialogMessageService
    {
        Task<bool?> ShowMessage(string message, string title = "提示", DialogButton dialogButton = DialogButton.Sumit, Action<IDialog> action = null, Window owner = null);
        Task<bool?> Show(object presenter, Action<IDialog> action = null, DialogButton dialogButton = DialogButton.Sumit, string title = null, Func<bool> canSumit = null, Window owner = null);
        Task<bool?> ShowIoc(Type typeAction, Func<ICancelable, bool?> action = null, Action<IDialog> build = null, DialogButton dialogButton = DialogButton.Sumit, string title = null, Func<bool> canSumit = null, Window owner = null);
        Task<bool?> ShowIoc<T>(Func<ICancelable, T, bool?> action = null, Action<IDialog> build = null, DialogButton dialogButton = DialogButton.Sumit, string title = null, Func<bool> canSumit = null, Window owner = null);

        Task<T> ShowPercent<T>(Func<IPercentPresenter, ICancelable, T> action, Action<IDialog> build = null, DialogButton dialogButton = DialogButton.Cancel, string title = null, Window owner = null);
        Task<T> ShowString<T>(Func<IStringPresenter, ICancelable, T> action, Action<IDialog> build = null, DialogButton dialogButton = DialogButton.Cancel, string title = null, Window owner = null);
        Task<T> ShowWait<T>(Func<ICancelable, T> action, Action<IDialog> build = null, DialogButton dialogButton = DialogButton.Cancel, string title = null, Window owner = null);
    }
}
