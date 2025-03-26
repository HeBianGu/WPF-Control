
global using System.IO;
global using System.ComponentModel.DataAnnotations;
global using H.Common.Attributes;
global using H.Common.Commands;
global using H.Services.Message;
namespace H.Controls.FavoriteBox
{
    [Icon("\xE713")]
    [Display(Name = "编辑", Description = "显示编辑收藏夹页面")]
    public class EditFavoriteCommand : DisplayMarkupCommandBase
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
