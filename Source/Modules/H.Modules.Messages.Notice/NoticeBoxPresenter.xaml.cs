﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Services.Common;
using H.Mvvm;
using System.Collections.ObjectModel;

namespace H.Modules.Messages.Notice
{
    public class NoticeBoxPresenter : BindableBase
    {
        private ObservableCollection<INoticeItem> _collection = new ObservableCollection<INoticeItem>();
        public ObservableCollection<INoticeItem> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }
    }
}
