// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Services.Common;
using System.Reflection;

namespace H.Controls.FilterBox
{

    public class IntFilter : MathValueFilter<int>
    {
        public IntFilter()
        {

        }

        public IntFilter(PropertyInfo property) : base(property)
        {

        }

        public override IFilterable Copy()
        {
            return new IntFilter(this.PropertyInfo) { Operate = this.Operate, Value = this.Value };
        }
    }

    public class LongFilter : MathValueFilter<long>
    {
        public LongFilter()
        {

        }

        public LongFilter(PropertyInfo property) : base(property)
        {

        }

        public override IFilterable Copy()
        {
            return new LongFilter(this.PropertyInfo) { Operate = this.Operate, Value = this.Value };
        }
    }
}
