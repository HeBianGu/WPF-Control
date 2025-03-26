
namespace H.Controls.FavoriteBox
{
    [Icon("\xE713")]
    [Display(Name = "添加", Description = "显示添加收藏夹页面")]
    public class CreateFavoriteCommand : DisplayMarkupCommandBase
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
