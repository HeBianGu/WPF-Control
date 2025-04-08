using H.Common.Interfaces;
using H.Presenters.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace H.Modules.Messages.Dialog
{
    public class AdornerDialogMessageService : IDialogMessageService, IAdornerDialogMessageService
    {
        public async Task<bool?> Show(object presenter, Action<IDialog> builder = null, Func<Task<bool>> canSumit = null)
        {
            var data = presenter is string str ? new StringPresenter() { Value = str } : presenter;
            return await AdornerDialog.ShowPresenter(data, x =>
            {
                x.HorizontalAlignment = HorizontalAlignment.Center;
                x.VerticalAlignment = VerticalAlignment.Center;
                x.HorizontalContentAlignment = HorizontalAlignment.Center;
                x.VerticalContentAlignment = VerticalAlignment.Center;
                if (data is StringPresenter)
                {
                    x.MinWidth = 400;
                    x.Padding = new Thickness(20);
                }
                if (data is ITitleable titleable && !string.IsNullOrEmpty(titleable.Title))
                    x.Title = titleable.Title;
                else if (data is INameable nameable && !string.IsNullOrEmpty(nameable.Name))
                    x.Title = nameable.Name;
                else if (data is ITextable textable && !string.IsNullOrEmpty(textable.Text))
                    x.Title = textable.Text;
                if (data is IIconable iconable && !string.IsNullOrEmpty(iconable.Icon))
                    x.Icon = iconable.Icon;

                if (presenter is ILayoutable layoutable)
                    layoutable.CopyTo(x);
                if (presenter is IDesignPresenterBase designPresenter)
                    x.CopyFrom(designPresenter);
                builder?.Invoke(x);
            }, canSumit);
        }

        public async Task<T> ShowAction<P, T>(P presenter, Action<IDialog> builder = null, Func<IDialog, P, T> action = null)
        {
            return await AdornerDialog.ShowAction(presenter, action, builder);
        }

        public async Task<T> ShowPercent<T>(Func<IDialog, IPercentPresenter, T> action, Action<IDialog> build = null)
        {
            PercentPresenter p = new PercentPresenter();
            return await AdornerDialog.ShowAction(p, action, x =>
            {
                x.DialogButton = DialogButton.Cancel;
                x.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                x.HorizontalAlignment = HorizontalAlignment.Center;
                x.VerticalAlignment = VerticalAlignment.Center;
                x.MinWidth = 400;
                build?.Invoke(x);
            });
        }

        public async Task<T> ShowString<T>(Func<IDialog, IStringPresenter, T> action, Action<IDialog> build = null)
        {
            StringPresenter p = new StringPresenter();
            return await AdornerDialog.ShowAction(p, action, x =>
            {
                x.DialogButton = DialogButton.Cancel;
                x.HorizontalContentAlignment = HorizontalAlignment.Center;
                x.Padding = new Thickness(20);
                x.MinWidth = 300;
                build?.Invoke(x);
            });
        }

        public async Task<bool> ShowForeach<T>(Func<IEnumerable<T>> list, Func<T, Tuple<bool, string>> itemAction, Action<IDialog> build = null)
        {
            return await this.ShowString<bool>((d, m) =>
             {
                 m.Value = $"正在加载数据";
                 var finds = list.Invoke().ToList();
                 for (int i = 0; i < finds.Count; i++)
                 {
                     if (d.IsCancel)
                         return false;
                     T item = finds[i];
                     var tuple = itemAction?.Invoke(item);
                     string success = tuple.Item1 ? "完成" : "错误";
                     m.Value = $"[{i + 1}/{finds.Count}] {tuple.Item2} {success}";
                 }
                 m.Value = $"操作完成[{finds.Count}]";
                 Thread.Sleep(500);
                 return true;
             }, build);
        }

        public async Task<T> ShowWait<T>(Func<IDialog, T> action, Action<IDialog> build = null)
        {
            return await AdornerDialog.ShowAction(new WaitPresenter(), (d, p) => action.Invoke(d), x =>
            {
                x.DialogButton = DialogButton.Cancel;
                x.HorizontalAlignment = HorizontalAlignment.Center;
                x.VerticalAlignment = VerticalAlignment.Center;
                x.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                x.VerticalContentAlignment = VerticalAlignment.Center;
                x.Width = 400;
                build?.Invoke(x);
            });
        }
    }

    public static class LayoutableExtension
    {
        public static void CopyFrom(this ILayoutable layoutable, IDesignPresenterBase from)
        {
            layoutable.HorizontalAlignment = from.HorizontalAlignment;
            layoutable.VerticalAlignment = from.VerticalAlignment;
            layoutable.HorizontalContentAlignment = from.HorizontalContentAlignment;
            layoutable.VerticalContentAlignment = from.VerticalContentAlignment;
            layoutable.Height = from.Height;
            layoutable.Width = from.Width;
            layoutable.Padding = from.Padding;
            layoutable.Margin = from.Margin;
            layoutable.MinWidth = from.MinWidth;
            layoutable.MinHeight = from.MinHeight;
            layoutable.BorderBrush = from.BorderBrush;
            layoutable.BorderThickness = from.BorderThickness;
            layoutable.Background = from.Background;
            layoutable.IsEnabled = from.IsEnabled;
            layoutable.Opacity = from.Opacity;
        }
    }
}
