using H.Presenters.Common;
using H.Windows.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace H.Modules.Messages.Dialog
{
    public class WindowDialogMessageService : IDialogMessageService, IWindowDialogMessageService
    {
        public async Task<bool?> Show(object presenter, Action<IDialog> builder = null, Func<Task<bool>> canSumit = null)
        {
            var data = presenter is string str ? new StringPresenter() { Value = str } : presenter;
            bool? r = DialogWindow.ShowPresenter(data, builder, canSumit);
            return await Task.FromResult(r);
        }

        public async Task<T> ShowAction<P, T>(P presenter, Action<IDialog> builder = null, Func<IDialog, P, T> action = null)
        {
            T r = DialogWindow.ShowAction(presenter, action, builder);
            return await Task.FromResult(r);
        }

        public async Task<T> ShowPercent<T>(Func<IDialog, IPercentPresenter, T> action, Action<IDialog> build = null)
        {
            PercentPresenter p = new PercentPresenter();
            var r = DialogWindow.ShowAction(p, action, x =>
            {
                x.DialogButton = DialogButton.Cancel;
                build?.Invoke(x);
            });
            return await Task.FromResult(r);
        }

        public async Task<T> ShowString<T>(Func<IDialog, IStringPresenter, T> action, Action<IDialog> build = null)
        {
            StringPresenter p = new StringPresenter();
            T r = DialogWindow.ShowAction(p, action, x =>
            {
                x.HorizontalContentAlignment = HorizontalAlignment.Center;
                x.DialogButton = DialogButton.Cancel;
                build?.Invoke(x);
            });
            return await Task.FromResult(r);
        }

        public async Task<T> ShowWait<T>(Func<IDialog, T> action, Action<IDialog> build = null)
        {
            WaitPresenter p = new WaitPresenter();
            T r = DialogWindow.ShowAction(p, (d, p) => action.Invoke(d), x =>
            {
                x.DialogButton = DialogButton.Cancel;
                build?.Invoke(x);
            });
            return await Task.FromResult(r);
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
                m.Value = $"操作完成";
                Thread.Sleep(500);
                return true;
            }, build);
        }
    }
}
