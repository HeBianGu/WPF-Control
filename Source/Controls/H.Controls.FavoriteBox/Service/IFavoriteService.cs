using H.Common.Interfaces;

namespace H.Controls.FavoriteBox
{
    public interface IFavoriteService : IDataSource<IFavoriteItem>, ISplashLoad, ISplashSave
    {
        IFavoriteItem Create();
    }
}
