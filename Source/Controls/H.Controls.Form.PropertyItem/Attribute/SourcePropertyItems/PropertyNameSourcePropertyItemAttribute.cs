// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem
{
    public class PropertyNameSourcePropertyItemAttribute : SourcePropertyItemBaseAttribute
    {
        private PropertyInfo _sourcePropertyInfo;
        public PropertyNameSourcePropertyItemAttribute(Type type, string propertyName) : base(type)
        {
            this.PropertyName = propertyName;
        }

        public string PropertyName { get; set; }

        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            if (_sourcePropertyInfo == null)
                _sourcePropertyInfo = obj.GetType().GetProperty(this.PropertyName);
            return _sourcePropertyInfo.GetValue(obj) as IEnumerable;
        }
    }
}
