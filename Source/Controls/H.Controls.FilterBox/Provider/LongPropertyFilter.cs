// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.FilterBox
{

    public class LongPropertyFilter : ComparablePropertyFilterBase<long>
    {
        public LongPropertyFilter()
        {

        }

        public LongPropertyFilter(PropertyInfo property) : base(property)
        {

        }

        public override IFilterable Copy()
        {
            return new LongPropertyFilter(this.PropertyInfo) { Operate = this.Operate, Value = this.Value };
        }
    }
}
