using System.Collections.ObjectModel;

namespace H.Controls.FavoriteBox;
public interface IFavoriteOptions
{
    ObservableCollection<FavoriteItem> FavoriteItems { get; set; }
}