using H.Providers.Mvvm;
using System;

namespace H.Controls.TagBox
{
    public class TagsPresenter : NotifyPropertyChangedBase
    {
        public ITagService TagServce => Ioc.GetService<ITagService>();
    }
}
