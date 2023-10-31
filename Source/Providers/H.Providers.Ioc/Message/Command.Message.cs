// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows.Input;
using System.Windows.Markup;
using System;
using System.Windows.Controls;
using System.Threading;

namespace H.Providers.Ioc
{
    public abstract class MessageCommandBase : MarkupExtension, ICommand
    {
        public string Title { get; set; }
        public int Width { get; set; } = 500;
        public int Height { get; set; } = 300;

        public event EventHandler? CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }

        public abstract void Execute(object? parameter);

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        protected void Build(Control c)
        {
            c.Width = this.Width;
            c.Height = this.Height;
        }
    }

    public class ShowMessageCommand : MessageCommandBase
    {
        public string Message { get; set; }
        public override void Execute(object parameter)
        {
            IocMessage.Dialog.ShowMessage(this.Message, this.Title, this.Build);
        }
        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(this.Message);
        }
    }

    public class ShowCommand : MessageCommandBase
    {
        public object Presnter { get; set; }
        public override void Execute(object parameter)
        {
            IocMessage.Dialog.Show(this.Presnter, this.Build, this.Title);
        }
        public override bool CanExecute(object parameter)
        {
            return this.Presnter != null;
        }
    }

    public class ShowIocCommand : MessageCommandBase
    {
        public Type Type { get; set; }
        public override void Execute(object parameter)
        {
            IocMessage.Dialog.ShowIoc(this.Type, this.Build, this.Title);
        }

        public override bool CanExecute(object parameter)
        {
            return this.Type != null;
        }
    }

    public class ShowPercentCommand : MessageCommandBase
    {
        public override void Execute(object parameter)
        {
            Func<IPercentPresenter, ICancelable, bool> func = (p, c) =>
                {
                    for (int i = 0; i <= 100; i++)
                    {
                        if (c.IsCancel)
                            break;
                        p.Value = i;
                        Thread.Sleep(50);
                    }
                    return true;
                };
            IocMessage.Dialog.ShowPercent(func);
        }
    }

    public class ShowStringCommand : MessageCommandBase
    {
        public string Format { get; set; } = "正在加载数据第{0}/100条";
        public override void Execute(object parameter)
        {
            Func<IStringPresenter, ICancelable, bool> func = (p, c) =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    if (c.IsCancel)
                        break;
                    p.Value = string.Format(this.Format, i);
                    Thread.Sleep(100);
                }
                p.Value = c.IsCancel ? "取消操作" : "加载完成";
                Thread.Sleep(1000);
                return true;
            };
            IocMessage.Dialog.ShowString(func);
        }
    }

    public class ShowWaitCommand : MessageCommandBase
    {
        public override void Execute(object parameter)
        {
            Func<ICancelable, bool> func = c =>
            {
                Thread.Sleep(5000);
                return true;
            };
            IocMessage.Dialog.ShowWait(func);
        }
    }

    public class ShowEditCommand : MessageCommandBase
    {
        public object Value { get; set; }
        public override void Execute(object parameter)
        {
            IocMessage.Form.ShowEdit(this.Value);
        }
    }

    public class ShowViewCommand : MessageCommandBase
    {
        public object Value { get; set; }
        public override void Execute(object parameter)
        {
            IocMessage.Form.ShowView(this.Value);
        }
    }
}
