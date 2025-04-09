// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System.Reflection;

namespace H.Controls.FilterBox
{
    public class IntPropertyFilter : ComparablePropertyFilterBase<int>
    {
        public IntPropertyFilter()
        {

        }

        public IntPropertyFilter(PropertyInfo property) : base(property)
        {

        }

        public override IFilterable Copy()
        {
            return new IntPropertyFilter(this.PropertyInfo) { Operate = this.Operate, Value = this.Value };
        }
    }
}
