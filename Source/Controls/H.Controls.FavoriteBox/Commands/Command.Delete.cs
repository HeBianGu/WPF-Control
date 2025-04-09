using H.Mvvm;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
namespace H.Controls.FavoriteBox
{
    [Icon("\xE713")]
    [Display(Name = "删除", Description = "删除收藏")]
    public class DeleteFavoriteCommand : DisplayMarkupCommandBase
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
