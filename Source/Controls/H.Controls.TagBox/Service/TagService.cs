using H.Providers.Mvvm;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Documents;

namespace H.Controls.TagBox
{
    [Display(Name = "标签管理")]
    public class TagService : ViewModelBase, ITagService
    {
        IOptions<TagOptions> _options;

        public TagService(IOptions<TagOptions> options)
        {
            _options = options;
        }
        public string Name => "标签管理";
        private IEnumerable<ITag> _collection;
        public event EventHandler CollectionChanged;
        public virtual IEnumerable<ITag> Collection => this._options.Value.Tags;

        public virtual void Add(params ITag[] ts)
        {
            if (this.Collection is IList list)
            {
                foreach (var t in ts)
                {
                    if (t is Tag)
                        list.Add(t);
                }
                this.OnCollectionChanged();
            }
        }

        public virtual ITag Create()
        {
            return new Tag();
        }

        public virtual void Delete(params ITag[] ts)
        {
            if (this.Collection is IList list)
            {
                var tags = ts.OfType<Tag>();
                foreach (var t in tags)
                {
                    list.Remove(t);
                }
                this.OnCollectionChanged();
            }
        }

        public virtual bool Load(out string message)
        {
            message = string.Empty;
            var r = this._options.Value.Load(out message);
            this.OnCollectionChanged();
            return r;
        }

        public virtual bool Save(out string message)
        {
            return this._options.Value.Save(out message);
        }


        public virtual string ConvertToCheck(string name, ITag tag)
        {
            var list = ToArray(name)?.ToList();
            var contain = list?.Contains(tag.Name) == true;
            if (contain)
                return name;
            name += " " + tag.Name;
            name.Trim(' ');
            return name;
        }

        private string[] ToArray(string name) => name?.Split(TagOptions.Instance.SplitChars.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

        public virtual string ConvertToUnCheck(string name, ITag tag)
        {
            var list = ToArray(name)?.ToList();
            var contain = list?.Contains(tag.Name) == true;
            if (!contain)
                return name;
            list.Remove(tag.Name);
            return string.Join(' ', list).Trim(' ');
        }

        public virtual bool ContainTag(string name, ITag tag)
        {
            var list = ToArray(name)?.ToList();
            return list?.Contains(tag.Name) == true;
        }

        public virtual IEnumerable<ITag> ToTags(string name)
        {
            var list = ToArray(name)?.ToList();
            return this.Collection.Where(x => list != null && list.Any(k => k == x.Name));
        }

        protected void OnCollectionChanged()
        {
            this.CollectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
