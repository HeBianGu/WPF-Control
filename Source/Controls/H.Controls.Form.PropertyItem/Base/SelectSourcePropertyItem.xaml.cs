// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Controls.Form.PropertyItems.Base;
global using H.Extensions.Common;
global using System.Collections.Generic;
global using System.Collections.ObjectModel;
global using System.Diagnostics;
global using System.Linq;
using H.Controls.Form.PropertyItem.Attribute;

namespace H.Controls.Form.PropertyItem.Base
{
    public class SelectSourceItem<T>
    {
        public SelectSourceItem(T key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
        public T Key { get; set; }
        public string Value { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Key);
        }

        public override bool Equals(object obj)
        {
            if (obj is SelectSourceItem<T> item)
                return this.Key.Equals(item.Key);
            return false;
        }
    }

    public abstract class SelectSourcePropertyItem<T> : ObjectPropertyItem<T>, ISelectSourcePropertyItem
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
            var source = this.PropertyInfo.GetCustomAttribute<GetSourceAttribute>();
            if (source == null)
                return null;
            IEnumerable items = source.GetSource(this.PropertyInfo, this.Obj);
            return items.OfType<T>();
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

        public void RefreshSource()
        {
            this.Collection = this.CreateSource()?.ToObservable();
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
