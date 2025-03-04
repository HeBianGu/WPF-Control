using H.Mvvm.ViewModels.Base;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace H.Controls.FavoriteBox
{
    [Display(Name = "收藏夹管理")]
    public class FavoriteService : BindableBase, IFavoriteService
    {
        IOptions<FavoriteOptions> _options;

        public FavoriteService(IOptions<FavoriteOptions> options)
        {
            _options = options;
        }
        public string Name => "收藏夹管理";
        private IEnumerable<IFavoriteItem> _collection;
        public event EventHandler CollectionChanged;
        public virtual IEnumerable<IFavoriteItem> Collection => this._options.Value.FavoriteItems;

        public virtual void Add(params IFavoriteItem[] ts)
        {
            if (this.Collection is IList list)
            {
                foreach (var item in ts.OfType<FavoriteItem>())
                {
                    list.Add(item);
                }
            }
            this.CollectionChanged?.Invoke(this, EventArgs.Empty);
        }

        public virtual IFavoriteItem Create()
        {
            return new FavoriteItem();
        }

        public virtual void Delete(params IFavoriteItem[] ts)
        {
            var favorites = ts.OfType<FavoriteItem>();
            foreach (var t in favorites)
            {
                if (this.Collection is IList list)
                    list.Remove(t);
            }
            this.CollectionChanged?.Invoke(this, EventArgs.Empty);
        }

        public virtual bool Load(out string message)
        {
            message = string.Empty;
            var r = this._options.Value.Load(out message);
            this.CollectionChanged?.Invoke(this, EventArgs.Empty);
            return r;
        }

        public virtual bool Save(out string message)
        {
            this.CollectionChanged?.Invoke(this, EventArgs.Empty);
            return this._options.Value.Save(out message);
        }
    }
}
