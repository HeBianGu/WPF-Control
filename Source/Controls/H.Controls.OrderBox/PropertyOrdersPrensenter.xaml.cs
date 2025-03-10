// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Services.Common;
using H.Mvvm;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using H.Mvvm.ViewModels.Base;

namespace H.Controls.OrderBox
{
    public class PropertyOrdersPrensenter : DisplayBindableBase, IOrderWhereable, IMetaSetting
    {
        public PropertyOrdersPrensenter()
        {

        }

        public PropertyOrdersPrensenter(Type modelTyle, Func<PropertyInfo, bool> predicate = null)
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
        [System.Text.Json.Serialization.JsonIgnore]
        
        [System.Xml.Serialization.XmlIgnore]
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
        [System.Text.Json.Serialization.JsonIgnore]
        
        [System.Xml.Serialization.XmlIgnore]
        public RelayCommand AddCommand => new RelayCommand(l =>
        {
            PropertyOrderPrensenter item = new PropertyOrderPrensenter() { ID = DateTime.Now.ToString("yyyyMMddHHmmssfff") };
            item.Properties = this.Properties;
            item.Load();
            this.PropertyOrders.Add(item);
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

        public bool Save(out string message)
        {
            message = null;
            if (string.IsNullOrEmpty(this.ID))
            {
                message = "ID为空";
                return false;
            }
            this.SelectedIndex = this.PropertyOrders.IndexOf(this.SelectedItem);
            this.MetaSettingService?.Serilize(this, this.ID);
            return true;
        }

        [System.Text.Json.Serialization.JsonIgnore]
        
        [System.Xml.Serialization.XmlIgnore]
        public IMetaSettingService MetaSettingService => new TextJsonMetaSettingService();

        private bool _loaded = false;
        public void Load()
        {
            if (_loaded == true)
                return;
            PropertyOrdersPrensenter find = this.MetaSettingService?.Deserilize<PropertyOrdersPrensenter>(this.ID);
            if (find == null)
                return;
            this.PropertyOrders = find.PropertyOrders;
            if (find.SelectedIndex >= 0)
                this.SelectedItem = find.PropertyOrders[find.SelectedIndex];

            foreach (PropertyOrderPrensenter item in find.PropertyOrders)
            {
                item.Properties = this.Properties;
                foreach (PropertyOrder Order in item.Conditions)
                {
                    PropertyInfo propertyInfo = item.Properties.FirstOrDefault(x => x.Name == Order.PropertyName);
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
