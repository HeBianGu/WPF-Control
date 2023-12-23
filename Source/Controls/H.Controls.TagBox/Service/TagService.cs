using H.Providers.Mvvm;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace H.Controls.TagBox
{
    [Display(Name = "标签管理")]
    public class TagService : NotifyPropertyChangedBase, ITagService
    {
        IOptions<TagOptions> _options;

        public TagService(IOptions<TagOptions> options)
        {
            _options = options;
        }
        public string Name => "标签管理";
        private IEnumerable<ITag> _collection;
        public event EventHandler CollectionChanged;
        public IEnumerable<ITag> Collection => this._options.Value.Tags;

        public void Add(params ITag[] ts)
        {
            this._options.Value.Tags.AddRange(ts.OfType<Tag>());
            this.CollectionChanged?.Invoke(this, EventArgs.Empty);
        }

        public ITag Create()
        {
            return new Tag();
        }

        public void Delete(params ITag[] ts)
        {
            var tags = ts.OfType<Tag>();
            foreach (var t in tags)
            {
                this._options.Value.Tags.Remove(t);
            }
            this.CollectionChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool Load(out string message)
        {
            message = string.Empty;
            this._options.Value.Load();
            this.CollectionChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }

        public bool Save(out string message)
        {
            this.CollectionChanged?.Invoke(this, EventArgs.Empty);
            return this._options.Value.Save(out message);
        }
    }
}
