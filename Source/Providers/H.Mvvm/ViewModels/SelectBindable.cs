﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Mvvm.ViewModels.Base;
using System.ComponentModel;

namespace H.Mvvm
{

    public partial class SelectBindable<T> : ModelBindable<T>, ISelectBindable
    {
        public SelectBindable(T t) : base(t)
        {

        }

        private bool _isSelected;
        [Browsable(false)]
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged();
            }
        }

    }
}
