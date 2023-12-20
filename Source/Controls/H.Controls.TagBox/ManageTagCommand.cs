using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;

namespace H.Controls.TagBox
{
    public class ManageTagCommand : MarkupCommandBase
    {
        public override async void Execute(object parameter)
        {
            var tag = new TagsPresenter();
            await IocMessage.Dialog.Show(tag);
            Ioc.GetService<ITagService>().Save(out string message);
        }

        public override bool CanExecute(object parameter)
        {
            return Ioc.Exist<ITagService>();
        }
    }
}
