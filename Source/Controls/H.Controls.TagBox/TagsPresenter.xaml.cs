using H.Providers.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace H.Controls.TagBox
{
    public class TagsPresenter : NotifyPropertyChangedBase
    {
        public ITagService TagServce => Ioc.GetService<ITagService>();
    }
}
