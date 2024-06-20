using H.Providers.Ioc;

namespace H.Controls.FavoriteBox
{
    public interface IFavoriteService : IDataSource<IFavoriteItem>, ISplashLoad, ISplashSave
    {
        IFavoriteItem Create();
    }
}
