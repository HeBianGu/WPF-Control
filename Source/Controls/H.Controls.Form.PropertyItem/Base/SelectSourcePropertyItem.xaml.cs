// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem;
global using H.Controls.Form.PropertyItems.Base;
global using H.Extensions.Common;
global using System.Collections.Generic;
global using System.Collections.ObjectModel;
global using System.Diagnostics;
global using System.Linq;
using H.Controls.Form.PropertyItem.Attribute;

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

            this.DisplayMemberPath = property.GetCustomAttribute<DisplayMemberPathAttribute>()?.Path;
            this.SelectedValuePath = property.GetCustomAttribute<SelectedValuePathAttribute>()?.Path;
        }
        protected virtual IEnumerable<T> CreateSource()
        {
            SourcePropertyItemBaseAttribute attr = this.PropertyInfo.GetCustomAttribute<SourcePropertyItemBaseAttribute>();
            if (attr == null)
            {
                Trace.Assert(false, "没有定义数据源");
                yield break;
            }
            IEnumerable result = attr.GetSource(this.PropertyInfo, this.Obj);
            foreach (object item in result)
            {
                if (item is T t)
                    yield return t;
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



        private string _selectedValuePath;
        public string SelectedValuePath
        {
            get { return _selectedValuePath; }
            set
            {
                _selectedValuePath = value;
                RaisePropertyChanged();
            }
        }


        private string _displayMemberPath;
        public string DisplayMemberPath
        {
            get { return _displayMemberPath; }
            set
            {
                _displayMemberPath = value;
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
