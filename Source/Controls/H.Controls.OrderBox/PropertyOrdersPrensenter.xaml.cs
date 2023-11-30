// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq;
using System.Xml.Serialization;
using H.Providers.Mvvm;
using H.Providers.Ioc;
using System.Text.Json.Serialization;
using System.Collections;

namespace H.Controls.OrderBox
{
    public class PropertyOrdersPrensenter : DisplayerViewModelBase, IOrder, IMetaSettingSerilize, IMetaSetting
    {
        public PropertyOrdersPrensenter()
        {

        }

        public PropertyOrdersPrensenter(Type modelTyle, Func<PropertyInfo, bool> predicate = null)
        {
            var ps = modelTyle.GetProperties().Where(x => x.PropertyType.IsPrimitive || x.PropertyType == typeof(DateTime) || x.PropertyType == typeof(string)).ToObservable();
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

        private ObservableCollection<PropertyOrderPrensenter> _propertyOrders = new ObservableCollection<PropertyOrderPrensenter>();
        public ObservableCollection<PropertyOrderPrensenter> PropertyOrders
        {
            get { return _propertyOrders; }
            set
            {
                _propertyOrders = value;
                RaisePropertyChanged();
            }
        }

        private PropertyOrderPrensenter _selectedItem;
        [JsonIgnore]
        [XmlIgnore]
        public PropertyOrderPrensenter SelectedItem
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
            var item = new PropertyOrderPrensenter() { ID = DateTime.Now.ToString("yyyyMMddHHmmssfff") };
            item.Properties = this.Properties;
            item.Load();
            this.PropertyOrders.Add(item);
            if (this.SelectedItem == null)
                this.SelectedItem = item;
        });

        [JsonIgnore]
        [XmlIgnore]
        public RelayCommand ClearSelectionCommand => new RelayCommand(l =>
        {
            this.SelectedItem = null;
            this.Save();
        }, x => this.SelectedItem != null);

        public void Save()
        {
            if (string.IsNullOrEmpty(this.ID))
                return;
            this.SelectedIndex = this.PropertyOrders.IndexOf(this.SelectedItem);
            this.MetaSettingService?.Serilize(this, this.ID);
        }

        [JsonIgnore]
        [XmlIgnore]
        public IMetaSettingService MetaSettingService => new JsonMetaSettingService();
        bool _loaded = false;
        public void Load()
        {
            if (_loaded == true)
                return;
            var find = this.MetaSettingService?.Deserilize<PropertyOrdersPrensenter>(this.ID);
            if (find == null)
                return;
            this.PropertyOrders = find.PropertyOrders;
            if (find.SelectedIndex >= 0)
                this.SelectedItem = find.PropertyOrders[find.SelectedIndex];

            foreach (var item in find.PropertyOrders)
            {
                item.Properties = this.Properties;
                foreach (var Order in item.Conditions)
                {
                    var propertyInfo = item.Properties.FirstOrDefault(x => x.Name == Order.PropertyName);
                    Order.PropertyInfo = propertyInfo;
                }
            }
            _loaded = true;
        }

        public IEnumerable Where(IEnumerable from)
        {
            if (this.SelectedItem == null)
                return from;
            return this.SelectedItem.Where(from);
        }
    }
}
