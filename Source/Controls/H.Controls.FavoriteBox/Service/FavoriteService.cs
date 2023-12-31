using H.Extensions.Tree;
using H.Providers.Mvvm;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace H.Controls.FavoriteBox
{
    [Display(Name = "收藏夹管理")]
    public class FavoriteService : NotifyPropertyChangedBase, IFavoriteService
    {
        IOptions<FavoriteOptions> _options;

        public FavoriteService(IOptions<FavoriteOptions> options)
        {
            _options = options;
        }
        public string Name => "收藏夹管理";
        private IEnumerable<IFavoriteItem> _collection;
        public event EventHandler CollectionChanged;
        public IEnumerable<IFavoriteItem> Collection => this._options.Value.FavoriteItems;

        public void Add(params IFavoriteItem[] ts)
        {
            this._options.Value.FavoriteItems.AddRange(ts.OfType<FavoriteItem>());
            this.CollectionChanged?.Invoke(this, EventArgs.Empty);
        }

        public IFavoriteItem Create()
        {
            return new FavoriteItem();
        }

        public void Delete(params IFavoriteItem[] ts)
        {
            var favorites = ts.OfType<FavoriteItem>();
            foreach (var t in favorites)
            {
                this._options.Value.FavoriteItems.Remove(t);
            }
            this.CollectionChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool Load(out string message)
        {
            message = string.Empty;
            var r = this._options.Value.Load(out message);
            this.CollectionChanged?.Invoke(this, EventArgs.Empty);
            return r;
        }

        public bool Save(out string message)
        {
            this.CollectionChanged?.Invoke(this, EventArgs.Empty);
            return this._options.Value.Save(out message);
        }
    }

    public class FavoriteTree : ITree
    {
        public IEnumerable GetChildren(object parent)
        {
            if (parent == null)
                return IocFavoriteService.Instance.Collection.Where(x => !x.Path.Trim('\\').Contains(System.IO.Path.DirectorySeparatorChar));
            if (parent is IFavoriteItem directory)
                return IocFavoriteService.Instance.Collection.Where(x => Path.GetDirectoryName(x.Path) == directory.Path);
            return Enumerable.Empty<string>();
        }
    }
}
