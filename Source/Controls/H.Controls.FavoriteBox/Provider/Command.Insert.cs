using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.IO;

namespace H.Controls.FavoriteBox
{
    public class InsertFavoriteCommand : MarkupCommandBase
    {
        public override async void Execute(object parameter)
        {
            if (parameter is IFavoriteItem favoriteItem)
            {
                string parentPath = favoriteItem.Path;
                var service = Ioc.GetService<IFavoriteService>();
                var favorite = service.Create();
                var r = await IocMessage.Form.ShowEdit(favorite);
                if (r != true)
                    return;
                if (parentPath != null)
                    favorite.Path = Path.Combine(parentPath, favorite.Path);
                service.Add(favorite);
                service.Save(out string message);
            }

        }

        public override bool CanExecute(object parameter)
        {
            return Ioc.Exist<IFavoriteService>() && parameter is IFavoriteItem;
        }
    }
}
