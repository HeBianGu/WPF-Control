

namespace H.Controls.TagBox
{
    [Icon("\xE713")]
    [Display(Name = "导出标签", Description = "显示导出标签数据页面")]
    public class ExportTagsCommand : DisplayMarkupCommandBase
    {
        public override async void Execute(object parameter)
        {
            if (parameter is TagsPresenter tagsPresenter)
            {
                var service = Ioc.GetService<ITagService>();
                string txt = string.Join(",", service.Collection.Select(x => x.Name));
                TextBoxPresenter presenter = new TextBoxPresenter();
                presenter.Text = txt;
                await IocMessage.Dialog.Show(presenter);
            }
        }

        public override bool CanExecute(object parameter)
        {
            return Ioc.Exist<ITagService>() && parameter is TagsPresenter;
        }
    }
}
