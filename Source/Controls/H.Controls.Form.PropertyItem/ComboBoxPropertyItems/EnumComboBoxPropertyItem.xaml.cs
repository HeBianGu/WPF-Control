// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.Base;

namespace H.Controls.Form.PropertyItem.ComboBoxPropertyItems
{
    public class EnumComboBoxPropertyItem : SelectSourcePropertyItem<Enum>
    {
        public EnumComboBoxPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

        protected override IEnumerable<Enum> CreateSource()
        {
            if (!this.PropertyInfo.PropertyType.IsEnum)
                yield break;
            Array result = this.PropertyInfo.PropertyType.GetEnumValues();
            foreach (object item in result)
            {
                if (item is Enum @enum)
                    yield return @enum;
            }
        }
    }
}
