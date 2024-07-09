using H.Services.Common;
using H.Mvvm;
using System;
using System.IO;

namespace H.Controls.FavoriteBox
{
    public class EditFavoriteCommand : MarkupCommandBase
    {
        public override async void Execute(object parameter)
        {
            if (parameter is IFavoriteItem favoriteItem)
            {
                var service = Ioc.GetService<IFavoriteService>();
                string parentPath = Path.GetDirectoryName(favoriteItem.Path);
                string name = Path.GetFileNameWithoutExtension(favoriteItem.Path);
                favoriteItem.Path = name;
                var r = await IocMessage.Form.ShowEdit(favoriteItem);
                if (r != true)
                {
                    favoriteItem.Path = Path.Combine(parentPath, name);
                    return;
                }
                if (!string.IsNullOrEmpty(parentPath))
                    favoriteItem.Path = Path.Combine(parentPath, favoriteItem.Path);
                service.Save(out string message);
            }

        }

        public override bool CanExecute(object parameter)
        {
            return Ioc.Exist<IFavoriteService>() && parameter is IFavoriteItem;
        }
    }
}
