// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Mvvm.ViewModels;

namespace H.Controls.FilterBox
{
    [ContentProperty(nameof(Filter))]
    public class PropertyConfidtion : Bindable, IConditionable, IPropertyConfidtion
    {
        public PropertyConfidtion()
        {

        }

        public PropertyConfidtion(PropertyInfo propertyInfo)
        {
            this.Filter = FilterFactory.Create(propertyInfo, null) as IPropertyFilter;
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

        [System.Text.Json.Serialization.JsonIgnore]
        
        [System.Xml.Serialization.XmlIgnore]
        public RelayCommand SelectionChangedCommand => new RelayCommand(l =>
        {
            if (l is SelectionChangedEventArgs arg)
            {
                if (arg.AddedItems[0] is PropertyInfo info)
                    this.Filter = FilterFactory.Create(info, null) as IPropertyFilter;

            }
        });

        public bool IsMatch(object obj)
        {
            if (obj == null)
                return false;
            if (obj is IModelBindable mv)
                return this.Filter.IsMatch(mv.GetModel());
            return this.Filter.IsMatch(obj);
        }
    }
}
