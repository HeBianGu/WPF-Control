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
    public class ColorComboBoxPropertyItem : SelectSourcePropertyItem<Color>
    {
        public ColorComboBoxPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

        protected override IEnumerable<Color> CreateSource()
        {
            var source = base.CreateSource();
            return source ?? typeof(Colors).GetProperties().Select(x => x.GetValue(null)).OfType<Color>();
        }
    }
}
