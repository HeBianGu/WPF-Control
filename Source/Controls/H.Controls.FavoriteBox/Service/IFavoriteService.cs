using H.Services.Common;

namespace H.Controls.FavoriteBox
{
    public interface IFavoriteService : IDataSource<IFavoriteItem>, ISplashLoad, ISplashSave
    {
        IFavoriteItem Create();
    }
}
