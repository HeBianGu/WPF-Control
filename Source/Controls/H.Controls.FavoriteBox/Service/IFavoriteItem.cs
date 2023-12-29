using System.Windows.Media;

namespace H.Controls.FavoriteBox
{
    public interface IFavoriteItem
    {
        Brush Background { get; set; }
        string Description { get; set; }
        string Path { get; set; }
        string GroupName { get; set; }
    }
}
