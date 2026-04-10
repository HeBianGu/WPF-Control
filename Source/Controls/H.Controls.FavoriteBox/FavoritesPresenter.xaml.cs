// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Mvvm.ViewModels.Base;
global using System.Collections.ObjectModel;
using H.Extensions.Mvvm.ViewModels.Base;

namespace H.Controls.FavoriteBox
{
    [Icon("\xE713")]
    [Display(Name = "管理收藏夹", Description = "显示管理收藏夹页面")]
    public class FavoritesPresenter : DisplayBindableBase
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
