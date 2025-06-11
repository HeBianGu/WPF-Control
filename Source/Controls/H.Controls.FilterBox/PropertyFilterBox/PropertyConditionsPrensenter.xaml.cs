// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.Commands;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace H.Controls.FilterBox
{
    public abstract class PropertyConditionsPrensenterBase : DisplayBindableBase
    {

    }

    public abstract class PropertyConditionsPrensenter<T> : PropertyConditionsPrensenterBase where T : IPropertyConditionPrensenter
    {
        private ObservableCollection<T> _propertyConfidtions = new ObservableCollection<T>();
        public ObservableCollection<T> PropertyConfidtions
        {
            get { return _propertyConfidtions; }
            set
            {
                _propertyConfidtions = value;
                RaisePropertyChanged();
            }
        }


        private T _selectedItem;
        [JsonIgnore]
        [XmlIgnore]
        public T SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        public RelayCommand AddCommand => new RelayCommand(l =>
        {
            var item = this.Create();
            this.PropertyConfidtions.Add(item);
            if (this.SelectedItem == null)
                this.SelectedItem = item;
        });

        protected abstract T Create();
    }

    public class PropertyConditionsPrensenter : PropertyConditionsPrensenter<PropertyConditionPrensenter>, IConditionable, IMetaSetting
    {
        public PropertyConditionsPrensenter()
        {

        }

        public PropertyConditionsPrensenter(Type modelTyle, Func<PropertyInfo, bool> predicate = null)
        {
            ObservableCollection<PropertyInfo> ps = modelTyle.GetProperties().Where(x => x.PropertyType.IsPrimitive || x.PropertyType == typeof(DateTime) || x.PropertyType == typeof(string)).ToObservable();
            if (predicate != null)
                this.Properties = ps.Where(predicate).ToObservable();
            else
                this.Properties = ps.ToObservable();
        }

        private ObservableCollection<PropertyInfo> _properties = new ObservableCollection<PropertyInfo>();
        [JsonIgnore]
        [XmlIgnore]
        public ObservableCollection<PropertyInfo> Properties
        {
            get { return _properties; }
            set
            {
                _properties = value;
                RaisePropertyChanged();
            }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                RaisePropertyChanged();
            }
        }

        protected override PropertyConditionPrensenter Create()
        {
            var result = new PropertyConditionPrensenter() { ID = DateTime.Now.ToString("yyyyMMddHHmmssfff") };
            result.Properties = this.Properties;
            result.Load();
            return result;
        }

        [JsonIgnore]
        [XmlIgnore]
        public RelayCommand ClearSelectionCommand => new RelayCommand(l =>
        {
            this.SelectedItem = null;
            this.Save(out string message);
        }, x => this.SelectedItem != null);

        public bool IsMatch(object obj)
        {
            if (this.SelectedItem == null)
                return true;
            return this.SelectedItem.IsMatch(obj);
        }

        public bool Save(out string message)
        {
            message = null;
            if (string.IsNullOrEmpty(this.ID))
            {
                message = "ID为空";
                return false;
            }
            this.SelectedIndex = this.PropertyConfidtions.IndexOf(this.SelectedItem);
            this.MetaSettingService?.Serilize(this, this.ID);
            return true;
        }

        [JsonIgnore]

        [XmlIgnore]
        public IMetaSettingService MetaSettingService => new NewtonsoftJsonMetaSettingService();

        private bool _loaded = false;
        public void Load()
        {
            if (_loaded == true)
                return;
            PropertyConditionsPrensenter find = this.MetaSettingService?.Deserilize<PropertyConditionsPrensenter>(this.ID);
            if (find == null)
                return;
            this.PropertyConfidtions = find.PropertyConfidtions;
            if (find.SelectedIndex >= 0)
                this.SelectedItem = find.PropertyConfidtions[find.SelectedIndex];

            foreach (PropertyConditionPrensenter item in find.PropertyConfidtions)
            {
                item.Properties = this.Properties;
                foreach (IPropertyConfidtion confidtion in item.Conditions)
                {
                    PropertyInfo propertyInfo = item.Properties.FirstOrDefault(x => x.Name == confidtion.Filter.PropertyName);
                    confidtion.Filter.PropertyInfo = propertyInfo;
                }
            }
            _loaded = true;
        }
    }
}
