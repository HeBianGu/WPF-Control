// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Extensions.XmlSerialize;
using H.Services.Common;
using H.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace H.Controls.FilterBox
{
    public class PropertyConfidtionsPrensenter : DisplayBindableBase, IConditionable, IMetaSetting
    {
        public PropertyConfidtionsPrensenter()
        {

        }

        public PropertyConfidtionsPrensenter(Type modelTyle, Func<PropertyInfo, bool> predicate = null)
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

        private ObservableCollection<PropertyConfidtionPrensenter> _propertyConfidtions = new ObservableCollection<PropertyConfidtionPrensenter>();
        public ObservableCollection<PropertyConfidtionPrensenter> PropertyConfidtions
        {
            get { return _propertyConfidtions; }
            set
            {
                _propertyConfidtions = value;
                RaisePropertyChanged();
            }
        }

        private PropertyConfidtionPrensenter _selectedItem;
        [JsonIgnore]
        [XmlIgnore]
        public PropertyConfidtionPrensenter SelectedItem
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

        [JsonIgnore]
        [XmlIgnore]
        public RelayCommand AddCommand => new RelayCommand(l =>
        {
            PropertyConfidtionPrensenter item = new PropertyConfidtionPrensenter() { ID = DateTime.Now.ToString("yyyyMMddHHmmssfff") };
            item.Properties = this.Properties;
            item.Load();
            this.PropertyConfidtions.Add(item);
            if (this.SelectedItem == null)
                this.SelectedItem = item;
        });

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
        public IMetaSettingService MetaSettingService => new XmlMetaSettingService();

        private bool _loaded = false;
        public void Load()
        {
            if (_loaded == true)
                return;
            PropertyConfidtionsPrensenter find = this.MetaSettingService?.Deserilize<PropertyConfidtionsPrensenter>(this.ID);
            if (find == null)
                return;
            this.PropertyConfidtions = find.PropertyConfidtions;
            if (find.SelectedIndex >= 0)
                this.SelectedItem = find.PropertyConfidtions[find.SelectedIndex];

            foreach (PropertyConfidtionPrensenter item in find.PropertyConfidtions)
            {
                item.Properties = this.Properties;
                foreach (IPropertyConfidtion confidtion in item.Conditions)
                {
                    PropertyInfo propertyInfo = item.Properties.FirstOrDefault(x => x.Name == confidtion.Filter.Name);
                    confidtion.Filter.PropertyInfo = propertyInfo;
                }
            }
            _loaded = true;
        }
    }
}
