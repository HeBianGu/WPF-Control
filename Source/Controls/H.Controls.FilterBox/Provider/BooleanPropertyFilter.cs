// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.FilterBox
{
    public class BooleanPropertyFilter : PropertyFilterBase<bool>
    {
        public BooleanPropertyFilter()
        {

        }
        public BooleanPropertyFilter(PropertyInfo property) : base(property)
        {

        }

        public override IFilterable Copy()
        {
            return new BooleanPropertyFilter(this.PropertyInfo) { Operate = this.Operate, Value = this.Value };
        }

        public override bool IsMatch(object obj)
        {
            PropertyInfo p = obj.GetType().GetProperty(this.PropertyName);
            if (p == null || !p.CanRead)
                return false;
            bool v = (bool)p.GetValue(obj);
            return v == this.Value;
        }
    }
}
