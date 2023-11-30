// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq;
using System.Xml.Serialization;
using H.Providers.Mvvm;
using H.Providers.Ioc;
using System.Text.Json.Serialization;
using H.Extensions.XmlSerialize;

namespace H.Controls.FilterBox
{
    public class PropertyConfidtionsPrensenter : DisplayerViewModelBase, IConditionable, IMetaSettingSerilize, IMetaSetting
    {
        public PropertyConfidtionsPrensenter()
        {

        }

        public PropertyConfidtionsPrensenter(Type modelTyle, Func<PropertyInfo, bool> predicate = null)
        {
            var ps = modelTyle.GetProperties().Where(x => x.PropertyType.IsPrimitive || x.PropertyType == typeof(DateTime) || x.PropertyType == typeof(string)).ToObservable();
            if (predicate != null)
                Properties = ps.Where(predicate).ToObservable();
            else
                Properties = ps.ToObservable();
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
            var item = new PropertyConfidtionPrensenter() { ID = DateTime.Now.ToString("yyyyMMddHHmmssfff") };
            item.Properties = Properties;
            item.Load();
            PropertyConfidtions.Add(item);
            if (SelectedItem == null)
                SelectedItem = item;
        });

        [JsonIgnore]
        [XmlIgnore]
        public RelayCommand ClearSelectionCommand => new RelayCommand(l =>
        {
            SelectedItem = null;
            Save();
        }, x => SelectedItem != null);

        public bool IsMatch(object obj)
        {
            if (SelectedItem == null)
                return true;
            return SelectedItem.IsMatch(obj);
        }

        public void Save()
        {
            if (string.IsNullOrEmpty(ID))
                return;
            SelectedIndex = PropertyConfidtions.IndexOf(SelectedItem);
            MetaSettingService?.Serilize(this, ID);
        }

        [JsonIgnore]
        [XmlIgnore]
        public IMetaSettingService MetaSettingService => new XmlMetaSettingService();

        bool _loaded = false;
        public void Load()
        {
            if (_loaded == true)
                return;
            var find = MetaSettingService?.Deserilize<PropertyConfidtionsPrensenter>(ID);
            if (find == null)
                return;
            PropertyConfidtions = find.PropertyConfidtions;
            if (find.SelectedIndex >= 0)
                SelectedItem = find.PropertyConfidtions[find.SelectedIndex];

            foreach (var item in find.PropertyConfidtions)
            {
                item.Properties = Properties;
                foreach (var confidtion in item.Conditions)
                {
                    var propertyInfo = item.Properties.FirstOrDefault(x => x.Name == confidtion.Filter.Name);
                    confidtion.Filter.PropertyInfo = propertyInfo;
                }
            }
            _loaded = true;
        }
    }
}
