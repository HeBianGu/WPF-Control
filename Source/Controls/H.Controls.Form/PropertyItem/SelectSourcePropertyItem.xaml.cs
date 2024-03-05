// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace H.Controls.Form
{
    ///// <summary>
    ///// 下拉样式，当选择项改变通知指定属性
    ///// </summary>
    //public class SelectedItemComboBoxPropertyItem : ObjectPropertyItem<object>
    //{
    //    public SelectedItemComboBoxPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    //    {
    //        this.SelectedItemPropertyName = property.GetCustomAttribute<BindingSelectedItemPropertyNameAttribute>()?.PropertyName;
    //    }

    //    public string SelectedItemPropertyName { get; private set; }

    //    public RelayCommand SelectedItemChangedCommand => new RelayCommand((s, e) =>
    //    {
    //        var property = this.PropertyInfo.DeclaringType.GetProperty(this.SelectedItemPropertyName);
    //        if (property == null)
    //        {
    //            Trace.Assert(false, "没有定义选中项要通知的属性");
    //            return;
    //        }
    //        property.SetValue(this.Obj, e);
    //    });
    //}

    //[System.AttributeUsage(System.AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    //sealed class BindingSelectedItemPropertyNameAttribute : System.Attribute
    //{
    //    public BindingSelectedItemPropertyNameAttribute(string propertyName)
    //    {
    //        PropertyName = propertyName;
    //    }

    //    public string PropertyName { get; }
    //}

    public abstract class SelectSourcePropertyItem<T> : ObjectPropertyItem<object>
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
            {
                BindingGetSelectSourceMethodAttribute attr = this.PropertyInfo.GetCustomAttribute<BindingGetSelectSourceMethodAttribute>();
                if (attr != null)
                {
                    MethodInfo p = this.Obj.GetType().GetMethod(attr.MethodName);
                    return p.Invoke(this.Obj, null) as IEnumerable<T>;
                }
            }

            {
                BindingGetSelectSourcePropertyAttribute attr = this.PropertyInfo.GetCustomAttribute<BindingGetSelectSourcePropertyAttribute>();
                if (attr != null)
                {
                    PropertyInfo p = this.Obj.GetType().GetProperty(attr.PropertyName);
                    return p.GetValue(this.Obj) as IEnumerable<T>;
                }
            }

            Trace.Assert(false, "没有定义数据源");
            return null;
        }

        private ObservableCollection<T> _collection = new ObservableCollection<T>();
        /// <summary> 说明  </summary>
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
            this.PropertyInfo.SetValue(Obj, value);
        }
    }


    public class ListBoxSelectSourcePropertyItem : SelectSourcePropertyItem<object>
    {
        public ListBoxSelectSourcePropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }
    }

    public class ComboBoxSelectSourcePropertyItem : SelectSourcePropertyItem<object>
    {
        public ComboBoxSelectSourcePropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }
    }

    public class OnlyComboBoxSelectSourcePropertyItem : SelectSourcePropertyItem<object>
    {
        public OnlyComboBoxSelectSourcePropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }
    }
}
