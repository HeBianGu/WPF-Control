// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.FilterBox
{
    public class DoublePropertyFilter : ComparablePropertyFilterBase<double>
    {
        public DoublePropertyFilter()
        {

        }
        public DoublePropertyFilter(PropertyInfo property) : base(property)
        {

        }

        public override IFilterable Copy()
        {
            return new DoublePropertyFilter(this.PropertyInfo) { Operate = this.Operate, Value = this.Value };
        }
    }

}
