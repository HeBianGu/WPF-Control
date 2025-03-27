// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Extensions.XmlSerialize;

using H.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using H.Extensions.NewtonsoftJson;
using H.Mvvm.ViewModels.Base;
using H.Mvvm.Commands;
using H.Services.Common.Serialize.Meta;

namespace H.Controls.FilterBox
{
    public class PropertyConditionsPrensenter : DisplayBindableBase, IConditionable, IMetaSetting
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
        [System.Text.Json.Serialization.JsonIgnore]
        
        [System.Xml.Serialization.XmlIgnore]
        public ObservableCollection<PropertyInfo> Properties
        {
            get { return _properties; }
            set
            {
                _properties = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<PropertyConditionPrensenter> _propertyConfidtions = new ObservableCollection<PropertyConditionPrensenter>();
        public ObservableCollection<PropertyConditionPrensenter> PropertyConfidtions
        {
            get { return _propertyConfidtions; }
            set
            {
                _propertyConfidtions = value;
                RaisePropertyChanged();
            }
        }

        private PropertyConditionPrensenter _selectedItem;
        [System.Text.Json.Serialization.JsonIgnore]
        
        [System.Xml.Serialization.XmlIgnore]
        public PropertyConditionPrensenter SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
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

        [System.Text.Json.Serialization.JsonIgnore]
        
        [System.Xml.Serialization.XmlIgnore]
        public RelayCommand AddCommand => new RelayCommand(l =>
        {
            PropertyConditionPrensenter item = new PropertyConditionPrensenter() { ID = DateTime.Now.ToString("yyyyMMddHHmmssfff") };
            item.Properties = this.Properties;
            item.Load();
            this.PropertyConfidtions.Add(item);
            if (this.SelectedItem == null)
                this.SelectedItem = item;
        });

        [System.Text.Json.Serialization.JsonIgnore]
        
        [System.Xml.Serialization.XmlIgnore]
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

        [System.Text.Json.Serialization.JsonIgnore]
        
        [System.Xml.Serialization.XmlIgnore]
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
