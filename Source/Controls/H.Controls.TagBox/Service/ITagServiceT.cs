using System.Collections.Generic;

namespace H.Controls.TagBox
{
    public interface ITagService<T> : ITagService where T : ITag
    {
        new IList<T> Collection { get; }

        void Add(params T[] ts);

        bool ContainTag(string name, T tag);

        string ConvertToCheck(string value, T tag);

        string ConvertToUnCheck(string value, T tag);

        new T Create();

        void Delete(params T[] ts);

        new IEnumerable<T> ToTags(string name);
    }
}
