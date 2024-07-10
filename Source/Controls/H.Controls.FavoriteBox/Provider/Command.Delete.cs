using H.Mvvm;
using System;
using System.IO;
using System.Linq;
using H.Iocs;
namespace H.Controls.FavoriteBox
{
    public class DeleteFavoriteCommand : MarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            if (parameter is IFavoriteItem favoriteItem)
            {
                var service = Ioc.GetService<IFavoriteService>();
                service.Delete(favoriteItem);
                var children = service.Collection.Where(x => Path.GetDirectoryName(x.Path) == favoriteItem.Path).ToArray();
                service.Delete(children);
                service.Save(out string message);
            }
        }

        public override bool CanExecute(object parameter)
        {
            return Ioc.Exist<IFavoriteService>() && parameter is IFavoriteItem;
        }
    }
}
