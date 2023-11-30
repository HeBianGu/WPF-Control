// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace H.Modules.Messages.Notice
{
    public abstract class MessagePresenterBase : NotifyPropertyChangedBase, INoticeItem
    {
        public string Time { get; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public Geometry Geometry { get; set; }
        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }
        public int Level { get; set; }
    }
}
