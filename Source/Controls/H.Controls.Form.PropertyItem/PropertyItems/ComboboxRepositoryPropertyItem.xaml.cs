// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using System;
using H.Extensions.DataBase;
using System.Collections;
using System.Reflection;

namespace H.Controls.Form.PropertyItem.PropertyItems
{
    public class ComboboxRepositoryPropertyItem : ObjectPropertyItem<object>
    {
        public ComboboxRepositoryPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
            Type rType = typeof(IStringRepository<>).MakeGenericType(property.PropertyType);
            object r = Ioc.Services.GetService(rType);
            MethodInfo[] method = rType.GetMethods(BindingFlags.Instance);
            IEnumerable source = rType.GetMethod("GetList").Invoke(r, new object[] { null }) as IEnumerable;

            this.ItemsSource = source;
            object v = property.GetValue(obj);

            if (v == null && source is IList list)
                this.Value = list.Count > 0 ? list[0] : null;
            this.Value = v ?? source;
        }

        private IEnumerable _itemsSource;
        /// <summary> 说明  </summary>
        public IEnumerable ItemsSource
        {
            get { return _itemsSource; }
            set
            {
                _itemsSource = value;
                RaisePropertyChanged("ItemsSource");
            }
        }
    }

    //public class ComboboxRepositoryPropertyItem<TEntity> : ObjectPropertyItem<object> where TEntity : StringEntityBase
    //{
    //    public ComboboxRepositoryPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    //    {
    //        var r = Ioc.GetService<IStringRepository<TEntity>>();
    //        IEnumerable source = r.GetList();
    //        this.ItemsSource = r.GetList();
    //        object v = property.GetValue(obj);
    //        if (v == null && source is IList list)
    //        {
    //            this.Value = list.Count > 0 ? list[0] : null;
    //        }
    //        this.Value = v ?? source;
    //    }

    //    private List<TEntity> _itemsSource;
    //    public List<TEntity> ItemsSource
    //    {
    //        get { return _itemsSource; }
    //        set
    //        {
    //            _itemsSource = value;
    //            RaisePropertyChanged("ItemsSource");
    //        }
    //    }
    //}
}
