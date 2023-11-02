// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using H.Providers.Ioc;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace H.Providers.Ioc
{
    public interface IDialogMessage
    {
        Task<bool?> ShowMessage(string message, string title = "提示", DialogButton dialogButton = DialogButton.Sumit, Action<Control> action = null, Window owner = null);
        Task<bool?> Show(object presenter, Action<Control> action = null, DialogButton dialogButton = DialogButton.Sumit, string title = null, Func<bool> canSumit = null, Window owner = null);
        Task<bool?> ShowIoc(Type type, Action<Control> action = null, string title = null, Func<bool> canSumit = null, Window owner = null);
        Task<bool?> ShowIoc<T>(Action<Control> action = null, string title = null, Func<bool> canSumit = null, Window owner = null);

        Task<T> ShowPercent<T>(Func<IPercentPresenter, ICancelable, T> action, Action<Control> build = null, string title = null, Window owner = null);
        Task<T> ShowString<T>(Func<IStringPresenter, ICancelable, T> action, Action<Control> build = null, string title = null, Window owner = null);
        Task<T> ShowWait<T>(Func<ICancelable, T> action, Action<Control> build = null, string title = null, Window owner = null);
    }

    public class Messager : Ioc<IDialogMessage>
    {

    }

    public interface ICancelable
    {
        bool IsCancel { get; }
    }

    public interface IPercentPresenter
    {
        int Value { set; }
    }

    public interface IStringPresenter
    {
        string Value { set; }
    }

    public enum DialogButton
    {
        Sumit, Cancel, None, SumitAndCancel
    }
}
