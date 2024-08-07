﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Services.Common;
using System.Reflection;

namespace H.Controls.FilterBox
{
    public class DoubleFilter : MathValueFilter<double>
    {
        public DoubleFilter()
        {

        }
        public DoubleFilter(PropertyInfo property) : base(property)
        {

        }

        public override IFilterable Copy()
        {
            return new DoubleFilter(this.PropertyInfo) { Operate = this.Operate, Value = this.Value };
        }
    }

}
