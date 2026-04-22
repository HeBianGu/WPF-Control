// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Text.Json.Serialization;

namespace H.Controls.FilterBox
{
    public class EnumPropertyFilter : PropertyFilterBase<string>
    {
        public EnumPropertyFilter()
        {

        }

        public EnumPropertyFilter(PropertyInfo property) : base(property)
        {
            this.EnumType = property.PropertyType;
        }

        private Type _EnumType;
        public Type EnumType
        {
            get { return _EnumType; }
            set
            {
                _EnumType = value;
                RaisePropertyChanged();
                this.UpdateEnumType();
            }
        }

        public void UpdateEnumType()
        {
            var names = this.EnumType.GetEnumNames().ToList();
            this.Collection = names.Select(x => (Enum)Enum.Parse(this.EnumType, x)).ToObservable();
            this.UpdateValue();
        }

        protected override void UpdateValue()
        {
            base.UpdateValue();
            if (string.IsNullOrEmpty(this.Value) || this.EnumType == null)
                this.SelectedEnum = null;
            else
            {
                try
                {
                    this.SelectedEnum = (Enum)Enum.Parse(this.EnumType, this.Value);
                }
                catch
                {
                    this.SelectedEnum = null;
                }
            }
        }

        private ObservableCollection<Enum> _collection = new ObservableCollection<Enum>();
        [JsonIgnore]
        public ObservableCollection<Enum> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }

        private Enum _SelectedEnum;
        [JsonIgnore]
        public Enum SelectedEnum
        {
            get { return _SelectedEnum; }
            set
            {
                if (_SelectedEnum == value)
                    return;
                _SelectedEnum = value;
                RaisePropertyChanged();
                if (this.Value == value?.ToString())
                    return;
                this.Value = value?.ToString();
            }
        }

        public override IFilterable Copy()
        {
            return new EnumPropertyFilter(this.PropertyInfo) { Operate = this.Operate, Value = this.Value };
        }

        public override bool IsMatch(object obj)
        {
            if (obj == null)
                return false;
            PropertyInfo p = obj.GetType().GetProperty(this.PropertyName);
            if (p == null || !p.CanRead)
                return false;
            object propertyValue = p.GetValue(obj);
            if (propertyValue == null)
                return false;
            if (!propertyValue.GetType().IsEnum)
                return false;
            Enum enumValue = (Enum)propertyValue;
            string filterValue = this.Value;
            if (filterValue == null)
                return false;
            return enumValue.ToString() == filterValue;
        }
    }
}
