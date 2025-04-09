// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System.Reflection;

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
