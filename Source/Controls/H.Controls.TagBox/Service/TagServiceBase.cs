
namespace H.Controls.TagBox
{
    public abstract class TagServiceBase<T> : BindableBase, ITagService<T>, ITagService where T : ITag, new()
    {
        public abstract IList<T> Collection { get; }

        public string Name => "标签管理";

        IEnumerable<ITag> IDataSource<ITag>.Collection => (IEnumerable<ITag>)this.Collection;

        public event EventHandler CollectionChanged;

        public void Add(params T[] ts)
        {
            foreach (var item in ts)
            {
                this.Collection.Add(item);
            }
            this.OnCollectionChanged();
        }

        void IDataSource<ITag>.Add(params ITag[] ts)
        {
            this.Add(ts.OfType<T>().ToArray());
        }

        public bool ContainTag(string name, T tag)
        {
            var list = ToArray(name)?.ToList();
            return list?.Contains(tag.Name) == true;
        }

        private string[] ToArray(string name) => name?.Split(TagOptions.Instance.SplitChars.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);


        bool ITagService.ContainTag(string name, ITag tag) => this.ContainTag(name, (T)tag);

        public string ConvertToCheck(string value, T tag)
        {
            var list = ToArray(value)?.ToList();
            var contain = list?.Contains(tag.Name) == true;
            if (contain)
                return value;
            if (list == null)
                return tag.Name;
            list.Add(tag.Name);
            return string.Join(' ', list).Trim(' ');
        }

        string ITagService.ConvertToCheck(string value, ITag tag) => this.ConvertToCheck(value, (T)tag);

        public string ConvertToUnCheck(string value, T tag)
        {
            var list = ToArray(value)?.ToList();
            var contain = list?.Contains(tag.Name) == true;
            if (!contain)
                return value;
            list.Remove(tag.Name);
            return string.Join(' ', list).Trim(' ');
        }

        string ITagService.ConvertToUnCheck(string value, ITag tag) => this.ConvertToUnCheck(value, (T)tag);

        public T Create()
        {
            return new T();
        }

        public void Delete(params T[] ts)
        {
            foreach (var t in ts)
            {
                this.Collection.Remove(t);
            }
            this.OnCollectionChanged();
        }

        void IDataSource<ITag>.Delete(params ITag[] ts) => this.Delete(ts.OfType<T>().ToArray());

        protected void OnCollectionChanged()
        {
            this.CollectionChanged?.Invoke(this, EventArgs.Empty);
        }


        public IEnumerable<T> ToTags(string name)
        {
            var list = ToArray(name)?.ToList();
            return this.Collection.Where(x => list != null && list.Any(k => k == x.Name));
        }

        ITag ITagService.Create() => this.Create();

        IEnumerable<ITag> ITagService.ToTags(string name) => (IEnumerable<ITag>)this.ToTags(name);
        public abstract bool Load(out string message);
        public abstract bool Save(out string message);
    }
}
