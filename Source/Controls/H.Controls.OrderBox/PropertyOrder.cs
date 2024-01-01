// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Providers.Mvvm;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace H.Controls.OrderBox
{
    public class PropertyOrder : ViewModelBase, IProperty
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
