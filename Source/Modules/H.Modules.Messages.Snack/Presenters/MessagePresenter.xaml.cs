// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace H.Modules.Messages.Snack
{
    public abstract class MessagePresenterBase : NotifyPropertyChangedBase, ISnackItem
    {
        public string Time { get; } = DateTime.Now.ToString("HH:mm:ss");
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
