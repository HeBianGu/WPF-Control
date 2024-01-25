using H.Presenters.Common;
using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Linq;

namespace H.Controls.TagBox
{
    public class ExportTagsCommand : MarkupCommandBase
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
