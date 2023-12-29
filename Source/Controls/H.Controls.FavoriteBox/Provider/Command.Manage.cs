using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Linq;

namespace H.Controls.FavoriteBox
{
    public class ManageFavoriteCommand : MarkupCommandBase
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
