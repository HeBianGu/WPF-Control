// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Mvvm.ViewModels.Base;

namespace H.Presenters.Design.PrintPresenter
{
    public class PrintBoxPresenter : BindableBase
    {
        private ObservableCollection<IPrintPagePresenter> _collection = new ObservableCollection<IPrintPagePresenter>();
        /// <summary> 说明  </summary>
        public ObservableCollection<IPrintPagePresenter> Collection
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
