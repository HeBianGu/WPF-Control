using H.Providers.Ioc;
using System;
using System.Linq;

namespace H.Controls.TagBox
{
    public interface ITagService : IDataSourceService<ITag>, ISplashLoad, ISplashSave
    {
        ITag Create();
        string ConvertToCheck(string value, ITag tag);

        string ConvertToUnCheck(string value, ITag tag);
        bool ContainTag(string name, ITag tag);
    }

    public class IocTagService : Ioc<ITagService>
    {

    }
}
