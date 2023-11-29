// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase


using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;
using H.Providers.Mvvm;
using H.Providers.Ioc;
using System.Text.Json.Serialization;
using System.Linq;
using System.Windows.Input;
using System;
using System.Linq.Expressions;
using System.Collections;

namespace H.Controls.OrderBox
{
    public class PropertyOrder : NotifyPropertyChangedBase, IProperty
    {
        public PropertyOrder()
        {

        }
        public PropertyOrder(PropertyInfo propertyInfo)
        {
            this.PropertyInfo = propertyInfo;
            this.PropertyName = propertyInfo.Name;
        }


        private bool _isSelected = true;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged();
            }
        }

        private bool _useDesc;
        public bool UseDesc
        {
            get { return _useDesc; }
            set
            {
                _useDesc = value;
                RaisePropertyChanged();
            }
        }

        public string PropertyName { get; set; }

        private PropertyInfo _propertyInfo;
        [XmlIgnore]
        [JsonIgnore]
        public PropertyInfo PropertyInfo
        {
            get { return _propertyInfo; }
            set
            {
                _propertyInfo = value;
                RaisePropertyChanged();
                this.PropertyName = value.Name;
            }
        }
    }
}
