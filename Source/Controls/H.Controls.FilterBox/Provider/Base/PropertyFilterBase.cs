// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using System.Text.Json.Serialization;
using H.Mvvm.ViewModels.Base;
using EnumConverter = System.ComponentModel.EnumConverter;

namespace H.Controls.FilterBox
{
    public abstract class PropertyFilterBase<T> : Bindable, IPropertyFilter
    {
        public PropertyFilterBase()
        {

        }

        private ObservableCollection<T> _source = new ObservableCollection<T>();
        [System.Text.Json.Serialization.JsonIgnore]
        
        [System.Xml.Serialization.XmlIgnore]
        public ObservableCollection<T> Source
        {
            get { return _source; }
            set
            {
                _source = value;
                RaisePropertyChanged("Source");
            }
        }

        private ObservableCollection<T> _selectedSource = new ObservableCollection<T>();
        [System.Text.Json.Serialization.JsonIgnore]
        
        [System.Xml.Serialization.XmlIgnore]
        public ObservableCollection<T> SelectedSource
        {
            get { return _selectedSource; }
            set
            {
                _selectedSource = value;
                RaisePropertyChanged("SelectedSource");
            }
        }

        private PropertyInfo _propertyInfo;
        [System.Text.Json.Serialization.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public PropertyInfo PropertyInfo
        {
            get { return _propertyInfo; }
            set
            {
                _propertyInfo = value;
                RaisePropertyChanged();
            }
        }


        public PropertyFilterBase(PropertyInfo propertyInfo)
        {
            this.PropertyInfo = propertyInfo;

            this.PropertyName = propertyInfo.Name;

            string display = propertyInfo.GetCustomAttribute<DisplayAttribute>()?.Name;

            this.DisplayName = display ?? propertyInfo.Name;
        }

        public PropertyFilterBase(PropertyInfo property, IEnumerable source)
        {
            this.PropertyInfo = property;
            this.PropertyName = property.Name;
            string display = property.GetCustomAttribute<DisplayAttribute>()?.Name;
            this.DisplayName = display ?? property.Name;
            this.Source.Clear();
            if (source == null)
                return;
            List<T> finds = new List<T>();
            foreach (object item in source)
            {
                T v = (T)property.GetValue(item);
                finds.Add(v);
            }

            this.Source = finds.Distinct().ToObservable();
        }

        #region - 属性 -

        private string _propertyName;
        public string PropertyName
        {
            get { return _propertyName; }
            set
            {
                _propertyName = value;
                RaisePropertyChanged();
            }
        }


        private string _displayName;
        [System.Text.Json.Serialization.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                RaisePropertyChanged("DisplayName");
            }
        }

        private FilterOperate _operate;
        [TypeConverter(typeof(EnumConverter))]
        public FilterOperate Operate
        {
            get { return _operate; }
            set
            {
                _operate = value;
                RaisePropertyChanged("Operate");
            }
        }

        private T _value;
        public T Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged("Value");
            }
        }

        private bool _isSelected = true;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }

        #endregion

        #region - 命令 -

        #endregion

        #region - 方法 -

        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {


            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
        }

        #endregion

        public abstract bool IsMatch(object obj);

        public abstract IFilterable Copy();
    }
}
