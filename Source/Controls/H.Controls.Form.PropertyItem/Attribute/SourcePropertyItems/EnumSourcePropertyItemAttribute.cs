// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem
{
    public class EnumSourcePropertyItemAttribute : SourcePropertyItemBaseAttribute
    {
        public EnumSourcePropertyItemAttribute(Type type) : base(type)
        {

        }

        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            if (propertyInfo.PropertyType.IsEnum == false)
                return null;
            Array result = propertyInfo.PropertyType.GetEnumValues();
            return result;
        }
    }
}
