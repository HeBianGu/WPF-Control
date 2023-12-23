using H.Providers.Ioc;
using System;

namespace H.Controls.TagBox
{
    public interface ITagService : IDataSourceService<ITag>, ISplashLoad, ISplashSave
    {
        ITag Create();
    }

    public class IocTagService : Ioc<ITagService>
    {

    }
}
