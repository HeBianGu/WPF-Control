using H.Mvvm.ViewModels.Base;
using System.Collections.ObjectModel;

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
