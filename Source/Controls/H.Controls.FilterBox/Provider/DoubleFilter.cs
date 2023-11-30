// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Providers.Ioc;
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

        public override IFilter Copy()
        {
            return new DoubleFilter(this.PropertyInfo) { Operate = this.Operate, Value = this.Value };
        }
    }

}
