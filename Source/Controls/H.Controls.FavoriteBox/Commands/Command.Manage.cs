using H.Mvvm;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using H.Extensions.Common;

namespace H.Controls.FavoriteBox
{
    [Icon("\xE713")]
    [Display(Name = "管理收藏夹", Description = "显示管理收藏夹页面")]
    public class ManageFavoriteCommand : DisplayMarkupCommandBase
    {
        public override async void Execute(object parameter)
        {
            var ioc = Ioc.GetService<IFavoriteService>();
            var favorite = new FavoritesPresenter();
            var temp = ioc.Collection.Where(x => x.GroupName == parameter?.ToString()).ToList();
            favorite.Collection = temp.ToObservable();
            var r = await IocMessage.Dialog.Show(favorite);
            if (r != true)
                return;
            foreach (var item in temp)
            {
                if (favorite.Collection.Contains(item))
                    continue;
                ioc.Delete(item);
            }
            ioc.Save(out string message);
        }

        public override bool CanExecute(object parameter)
        {
            return Ioc.Exist<IFavoriteService>();
        }
    }
}
