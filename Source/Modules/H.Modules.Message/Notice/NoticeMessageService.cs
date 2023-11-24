// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using H.Controls.Adorner;
using H.Providers.Ioc;
using System;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;
using System.Linq;
using H.Providers.Mvvm;
using System.Windows.Controls;
using System.Diagnostics;
using System.Threading;

namespace H.Modules.Message
{
    public class NoticeMessageService : INoticeMessageService
    {
        private NoticeBoxPresenter _noticeBox = new NoticeBoxPresenter();
        void CheckValid()
        {
            var child = Application.Current.MainWindow.Content as UIElement;
            var layer = AdornerLayer.GetAdornerLayer(child);
            var adorners = layer.GetAdorners(child)?.OfType<PresenterAdorner>().Where(x => x.Presenter == this._noticeBox);
            if (adorners == null || adorners.Count() == 0)
            {
                var adorner = new PresenterAdorner(child, this._noticeBox);
                layer.Add(adorner);
            }
        }

        public async void ShowInfo(string message)
        {
            this.CheckValid();
            var presenter = new InfoMessagePresenter() { Message = message };
            this._noticeBox.Collection.Add(presenter);
            await Task.Run(() =>
            {
                Thread.Sleep(3000);
            });
            this._noticeBox.Collection.Remove(presenter);
        }

        public void ShowError(string message)
        {
            this.CheckValid();
            this._noticeBox.Collection.Add(new ErrorMessagePresenter() { Message = message });
        }
        public void Show(INoticeItem message)
        {
            this.CheckValid();
            this._noticeBox.Collection.Add(message);
        }

        public void ShowFatal(string message)
        {
            this.CheckValid();
            this._noticeBox.Collection.Add(new FatalMessagePresenter() { Message = message });
        }

        public async Task<bool?> ShowDialog(string message)
        {
            this.CheckValid();
            var dialog = new DialogMessagePresenter() { Message = message };
            this._noticeBox.Collection.Add(dialog);
            var r= await dialog.ShowDialog();
            this._noticeBox.Collection.Remove(dialog);
            return r;
        }

        public async Task<T> ShowProgress<T>(Func<IPercentNoticeItem, T> action)
        {
            this.CheckValid();
            var progress = new ProgressMessagePresenter();
            this._noticeBox.Collection.Add(progress);
            var r = await Task.Run(() => action.Invoke(progress));
            this._noticeBox.Collection.Remove(progress);
            return r;
        }

        public async Task<T> ShowString<T>(Func<INoticeItem, T> action)
        {
            this.CheckValid();
            var progress = new StringMessagePresenter();
            this._noticeBox.Collection.Add(progress);
            var r= await Task.Run(() => action.Invoke(progress));
            this._noticeBox.Collection.Remove(progress);
            return r;
        }

        public async void ShowSuccess(string message)
        {
            this.CheckValid();
            var presenter = new SuccessMessagePresenter() { Message = message };
            this._noticeBox.Collection.Add(presenter);
            await Task.Run(() =>
             {
                 Thread.Sleep(3000);
             });
            this._noticeBox.Collection.Remove(presenter);
        }

        public void ShowWarn(string message)
        {
            this.CheckValid();
            this._noticeBox.Collection.Add(new WarnMessagePresenter() { Message = message });

        }
    }
}
