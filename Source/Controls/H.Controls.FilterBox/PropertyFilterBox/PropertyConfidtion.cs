// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Providers.Mvvm;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace H.Controls.FilterBox
{
    public class PropertyConfidtion : NotifyPropertyChanged, IConditionable, IPropertyConfidtion
    {
        public PropertyConfidtion()
        {

        }

        public PropertyConfidtion(PropertyInfo propertyInfo)
        {
            Filter = FilterFactory.Create(propertyInfo, null) as IPropertyFilter;
        }

        private IPropertyFilter _filter;
        public IPropertyFilter Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                RaisePropertyChanged();
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        public RelayCommand SelectionChangedCommand => new RelayCommand(l =>
        {
            if (l is SelectionChangedEventArgs arg)
            {
                if (arg.AddedItems[0] is PropertyInfo info)
                    Filter = FilterFactory.Create(info, null) as IPropertyFilter;

            }
        });

        public bool IsMatch(object obj)
        {
            if (obj == null)
                return false;
            return Filter.IsMatch(obj);
        }
    }
}
