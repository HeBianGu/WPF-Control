// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Mvvm.ViewModels.Base;
global using System.Collections.ObjectModel;

namespace H.Controls.FavoriteBox
{
    public class FavoritesPresenter : BindableBase
    {
        private ObservableCollection<IFavoriteItem> _collection = new ObservableCollection<IFavoriteItem>();
        /// <summary> 说明  </summary>
        public ObservableCollection<IFavoriteItem> Collection
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
