// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Services.Common;
using System;
using System.Windows.Media;
using H.Mvvm.ViewModels.Base;

namespace H.Modules.Messages.Snack
{
    public abstract class MessagePresenterBase : DisplayBindableBase, ISnackItem
    {
        public string Time { get; } = DateTime.Now.ToString("HH:mm:ss");
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
