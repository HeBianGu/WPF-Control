using System.Collections.Generic;

namespace H.Controls.TagBox
{
    public interface IDataSourceService<T>
    {
        IEnumerable<T> Collection { get; }
        void Add(params T[] ts);
        void Delete(params T[] ts);
        bool Save(out string message);
        void Load();
    }
}
