// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Controls.Adorner.Adorner;
using H.Services.Logger;
using System.Windows;
using System.Windows.Documents;

namespace H.Modules.Messages.Notice
{

    public class NoticeMessageService : INoticeMessageService
    {
        private NoticeBoxPresenter _noticeBox = new NoticeBoxPresenter();

        private void CheckValid()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                UIElement child = PresenterAdorner.GetAdonerElement();
                AdornerLayer layer = AdornerLayer.GetAdornerLayer(child);
                if (layer == null)
                    return;
                System.Collections.Generic.IEnumerable<PresenterAdorner> adorners = layer.GetAdorners(child)?.OfType<PresenterAdorner>().Where(x => x.Presenter == this._noticeBox);
                //if (adorners == null || adorners.Count() == 0)
                //{
                //    PresenterAdorner adorner = new PresenterAdorner(child, this._noticeBox);
                //    layer.Add(adorner);
                //}
                if (adorners != null)
                {
                    foreach (var item in adorners)
                    {
                        layer.Remove(item);
                    }
                }
                PresenterAdorner adorner = new PresenterAdorner(child, this._noticeBox);
                layer.Add(adorner);
            });
        }

        public async void ShowInfo(string message)
        {
            IocLog.Info(message);
            this.CheckValid();
            var find = this._noticeBox.Collection.OfType<InfoMessagePresenter>().LastOrDefault(x => x.Message == message);
            if (find != null)
                this._noticeBox.Collection.Remove(find);
            InfoMessagePresenter presenter = new InfoMessagePresenter() { Message = message };
            this._noticeBox.Collection.Add(presenter);
            await Task.Delay(3000);
            this._noticeBox.Collection.Remove(presenter);
        }

        public void ShowError(string message)
        {
            IocLog.Info(message);
            this.CheckValid();
            var find = this._noticeBox.Collection.OfType<ErrorMessagePresenter>().LastOrDefault(x => x.Message == message);
            if (find != null)
                if (find != null)
                    this._noticeBox.Collection.Remove(find);
            this._noticeBox.Collection.Add(new ErrorMessagePresenter() { Message = message });
        }
        public void Show(INoticeItem message)
        {
            IocLog.Info(message.Message);
            this.CheckValid();
            this._noticeBox.Collection.Add(message);
        }

        public void ShowFatal(string message)
        {
            IocLog.Info(message);
            this.CheckValid();
            var find = this._noticeBox.Collection.OfType<FatalMessagePresenter>().LastOrDefault(x => x.Message == message);
            if (find != null)
                this._noticeBox.Collection.Remove(find);
            this._noticeBox.Collection.Add(new FatalMessagePresenter() { Message = message });
        }

        public async Task<bool?> ShowDialog(string message)
        {
            IocLog.Info(message);
            this.CheckValid();
            DialogMessagePresenter dialog = new DialogMessagePresenter() { Message = message };
            this._noticeBox.Collection.Add(dialog);
            bool? r = await dialog.ShowDialog();
            this._noticeBox.Collection.Remove(dialog);
            return r;
        }

        public async Task<T> ShowProgress<T>(Func<IPercentNoticeItem, T> action)
        {
            this.CheckValid();
            ProgressMessagePresenter progress = new ProgressMessagePresenter();
            this._noticeBox.Collection.Add(progress);
            T r = await Task.Run(() => action.Invoke(progress));
            this._noticeBox.Collection.Remove(progress);
            return r;
        }

        public async Task<T> ShowString<T>(Func<INoticeItem, T> action)
        {
            this.CheckValid();
            StringMessagePresenter progress = new StringMessagePresenter();
            this._noticeBox.Collection.Add(progress);
            T r = await Task.Run(() => action.Invoke(progress));
            this._noticeBox.Collection.Remove(progress);
            return r;
        }

        public async void ShowSuccess(string message)
        {
            IocLog.Info(message);
            this.CheckValid();
            var find = this._noticeBox.Collection.OfType<SuccessMessagePresenter>().LastOrDefault(x => x.Message == message);
            if (find != null)
                this._noticeBox.Collection.Remove(find);
            SuccessMessagePresenter presenter = new SuccessMessagePresenter() { Message = message };
            this._noticeBox.Collection.Add(presenter);
            await Task.Delay(3000);
            this._noticeBox.Collection.Remove(presenter);
        }

        public void ShowWarn(string message)
        {
            IocLog.Info(message);
            this.CheckValid();
            var find = this._noticeBox.Collection.OfType<WarnMessagePresenter>().LastOrDefault(x => x.Message == message);
            if (find != null)
                this._noticeBox.Collection.Remove(find);
            this._noticeBox.Collection.Add(new WarnMessagePresenter() { Message = message });
        }
    }
}
