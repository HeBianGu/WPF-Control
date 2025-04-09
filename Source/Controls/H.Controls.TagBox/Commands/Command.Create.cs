
namespace H.Controls.TagBox
{
    [Icon("\xE713")]
    [Display(Name = "床架标签", Description = "显示创建标签页面")]
    public class CreateTagCommand : DisplayMarkupCommandBase
    {
        public override async void Execute(object parameter)
        {
            var service = Ioc.GetService<ITagService>();
            var tag = service.Create();
            tag.GroupName = parameter?.ToString();
            var r = await IocMessage.Form.ShowEdit(tag);
            if (r != true)
                return;
            service.Add(tag);
            service.Save(out string message);
        }

        public override bool CanExecute(object parameter)
        {
            return Ioc.Exist<ITagService>();
        }
    }
}
