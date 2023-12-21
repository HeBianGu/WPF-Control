using H.Providers.Ioc;

namespace H.Controls.TagBox
{
    public interface ITagService : IDataSourceService<ITag>
    {
        ITag Create();
    }
}
