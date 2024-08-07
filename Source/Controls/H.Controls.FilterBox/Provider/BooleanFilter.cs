﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Services.Common;
using System.Reflection;

namespace H.Controls.FilterBox
{
    public class BooleanFilter : PropertyFilterBase<bool>
    {
        public BooleanFilter()
        {

        }
        public BooleanFilter(PropertyInfo property) : base(property)
        {

        }

        public override IFilterable Copy()
        {
            return new BooleanFilter(this.PropertyInfo) { Operate = this.Operate, Value = this.Value };
        }

        public override bool IsMatch(object obj)
        {
            PropertyInfo p = obj.GetType().GetProperty(this.Name);
            if (p == null || !p.CanRead)
                return false;
            bool v = (bool)p.GetValue(obj);
            return v == this.Value;
        }
    }
}
