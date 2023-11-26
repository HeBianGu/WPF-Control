// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using H.Controls.Adorner;
using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace H.Modules.Messages.Snack
{
    public class SnackMessageService : ISnackMessageService
    {
        private SnackBoxPresenter _snackBox = new SnackBoxPresenter();
        void CheckValid()
        {
            var child = Application.Current.MainWindow.Content as UIElement;
            var layer = AdornerLayer.GetAdornerLayer(child);
            var adorners = layer.GetAdorners(child)?.OfType<PresenterAdorner>().Where(x => x.Presenter == this._snackBox);
            if (adorners == null || adorners.Count() == 0)
            {
                var adorner = new PresenterAdorner(child, this._snackBox);
                layer.Add(adorner);
            }
        }

        public async void ShowInfo(string message)
        {
            this.CheckValid();
            var presenter = new InfoMessagePresenter() { Message = message };
            this._snackBox.Collection.Add(presenter);
            await Task.Run(() =>
            {
                Thread.Sleep(3000);
            });
            this._snackBox.Collection.Remove(presenter);
        }

        public void ShowError(string message)
        {
            this.CheckValid();
            this._snackBox.Collection.Add(new ErrorMessagePresenter() { Message = message });
        }
        public void Show(ISnackItem message)
        {
            this.CheckValid();
            this._snackBox.Collection.Add(message);
        }

        public void ShowFatal(string message)
        {
            this.CheckValid();
            this._snackBox.Collection.Add(new FatalMessagePresenter() { Message = message });
        }

        public async Task<bool?> ShowDialog(string message)
        {
            this.CheckValid();
            var dialog = new DialogMessagePresenter() { Message = message };
            this._snackBox.Collection.Add(dialog);
            var r = await dialog.ShowDialog();
            this._snackBox.Collection.Remove(dialog);
            return r;
        }

        public async Task<T> ShowProgress<T>(Func<IPercentSnackItem, T> action)
        {
            this.CheckValid();
            var progress = new ProgressMessagePresenter();
            this._snackBox.Collection.Add(progress);
            var r = await Task.Run(() => action.Invoke(progress));
            this._snackBox.Collection.Remove(progress);
            return r;
        }

        public async Task<T> ShowString<T>(Func<ISnackItem, T> action)
        {
            this.CheckValid();
            var progress = new StringMessagePresenter();
            this._snackBox.Collection.Add(progress);
            var r = await Task.Run(() => action.Invoke(progress));
            this._snackBox.Collection.Remove(progress);
            return r;
        }

        public async void ShowSuccess(string message)
        {
            this.CheckValid();
            var presenter = new SuccessMessagePresenter() { Message = message };
            this._snackBox.Collection.Add(presenter);
            await Task.Run(() =>
             {
                 Thread.Sleep(3000);
             });
            this._snackBox.Collection.Remove(presenter);
        }

        public void ShowWarn(string message)
        {
            this.CheckValid();
            this._snackBox.Collection.Add(new WarnMessagePresenter() { Message = message });

        }
    }
}
