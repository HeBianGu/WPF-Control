using H.Extensions.Tree;
using H.Providers.Ioc;

namespace H.Controls.FavoriteBox
{
    public interface IFavoriteService : IDataSourceService<IFavoriteItem>, ISplashLoad, ISplashSave
    {
        IFavoriteItem Create();
    }
}
