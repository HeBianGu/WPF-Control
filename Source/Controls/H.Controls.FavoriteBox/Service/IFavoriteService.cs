using H.Common.Interfaces;

namespace H.Controls.FavoriteBox
{
    public interface IFavoriteService : IDataSource<IFavoriteItem>, ISplashLoadable, ISplashSave
    {
        IFavoriteItem Create();
    }
}
