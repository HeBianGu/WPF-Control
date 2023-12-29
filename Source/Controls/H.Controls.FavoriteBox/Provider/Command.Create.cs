using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;

namespace H.Controls.FavoriteBox
{
    public class CreateFavoriteCommand : MarkupCommandBase
    {
        public override async void Execute(object parameter)
        {
            var service = Ioc.GetService<IFavoriteService>();
            var favorite = service.Create();
            var r = await IocMessage.Form.ShowEdit(favorite);
            if (r != true)
                return;
            service.Add(favorite);
            service.Save(out string message);
        }

        public override bool CanExecute(object parameter)
        {
            return Ioc.Exist<IFavoriteService>();
        }
    }
}
