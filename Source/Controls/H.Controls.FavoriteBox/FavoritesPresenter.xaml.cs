﻿using H.Providers.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace H.Controls.FavoriteBox
{
    public class FavoritesPresenter : ViewModelBase
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
