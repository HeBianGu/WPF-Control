using H.Extensions.Setting;
using H.Providers.Ioc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;

namespace H.Controls.TagBox
{
    public interface ITagService : IDataSourceService<ITag>
    {
        ITag Create();
    }
}
