// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Collections;
using System.Reflection;

namespace H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem
{
    public class SourcePropertyItemAttribute : SourcePropertyItemBaseAttribute
    {
        public SourcePropertyItemAttribute(Type type, IEnumerable source) : base(type)
        {
            this.Source = source;
        }

        public IEnumerable Source { get; set; }

        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            return this.Source;
        }
    }
}
