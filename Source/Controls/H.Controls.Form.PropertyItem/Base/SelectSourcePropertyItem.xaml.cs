// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem;
global using H.Extensions.Common;
global using System.Collections.Generic;
global using System.Collections.ObjectModel;
global using System.Diagnostics;
global using System.Linq;
global using System.Reflection;
global using H.Controls.Form.PropertyItems.Base;

namespace H.Controls.Form.PropertyItem.Base
{
    public abstract class SelectSourcePropertyItem<T> : ObjectPropertyItem<T>
    {
        public SelectSourcePropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
            this.Collection = this.CreateSource()?.ToObservable();
            this.LoadValue();
            if (this.Value == null)
                this.Value = this.Collection.FirstOrDefault();
        }
        protected virtual IEnumerable<T> CreateSource()
        {
            //{
            //    BindingGetSelectSourceMethodAttribute attr = this.PropertyInfo.GetCustomAttribute<BindingGetSelectSourceMethodAttribute>();
            //    if (attr != null)
            //    {
            //        MethodInfo p = this.Obj.GetType().GetMethod(attr.MethodName);
            //        return p.Invoke(this.Obj, null) as IEnumerable<T>;
            //    }
            //}

            //{
            //    BindingGetSelectSourcePropertyAttribute attr = this.PropertyInfo.GetCustomAttribute<BindingGetSelectSourcePropertyAttribute>();
            //    if (attr != null)
            //    {
            //        PropertyInfo p = this.Obj.GetType().GetProperty(attr.PropertyName);
            //        return p.GetValue(this.Obj) as IEnumerable<T>;
            //    }
            //}

            {
                SourcePropertyItemBaseAttribute attr = this.PropertyInfo.GetCustomAttribute<SourcePropertyItemBaseAttribute>();
                if (attr == null)
                {
                    Trace.Assert(false, "没有定义数据源");
                    yield break;
                }
                System.Collections.IEnumerable result = attr.GetSource(this.PropertyInfo, this.Obj);
                foreach (object item in result)
                {
                    if (item is T t)
                        yield return t;
                }
            }
        }

        private ObservableCollection<T> _collection = new ObservableCollection<T>();
        public ObservableCollection<T> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }
    }

    public class SelectSourcePropertyItem : SelectSourcePropertyItem<object>
    {
        public SelectSourcePropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
            this.Collection = this.CreateSource()?.ToObservable();
            this.LoadValue();

        }

        protected override bool CheckType(object value, out string error)
        {
            error = null;

            return true;
        }

        protected override void SetValue(object value)
        {
            this.PropertyInfo.SetValue(this.Obj, value);
        }
    }
}
