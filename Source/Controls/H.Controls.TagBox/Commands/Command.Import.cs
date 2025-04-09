global using H.Presenters.Common;
using H.Presenters.Common;

namespace H.Controls.TagBox
{
    [Icon("\xE713")]
    [Display(Name = "导入标签", Description = "显示导入标签页面")]
    public class ImportTagsCommand : DisplayMarkupCommandBase
    {
        public override async void Execute(object parameter)
        {
            if (parameter is TagsPresenter tagsPresenter)
            {
                var service = Ioc.GetService<ITagService>();
                TextBoxPresenter presenter = new TextBoxPresenter();
                var r = await IocMessage.Dialog.Show(presenter);
                if (r != true)
                    return;
                if (string.IsNullOrEmpty(presenter.Text))
                    return;
                var items = presenter.Text.Split(TagOptions.Instance.SplitChars.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                string group = tagsPresenter.GroupName;
                foreach (var item in items)
                {
                    if (tagsPresenter.Collection.Any(x => x.Name == item))
                        continue;
                    var tag = service.Create();
                    tag.Name = item;
                    tag.GroupName = group;
                    tagsPresenter.Collection.Add(tag);
                }
            }
        }

        public override bool CanExecute(object parameter)
        {
            return Ioc.Exist<ITagService>() && parameter is TagsPresenter;
        }
    }
}
