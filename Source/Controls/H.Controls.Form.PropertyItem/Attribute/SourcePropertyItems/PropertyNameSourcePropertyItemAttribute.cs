// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Collections;
using System.Reflection;

namespace H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem
{
    public class PropertyNameSourcePropertyItemAttribute : SourcePropertyItemBaseAttribute
    {
        public PropertyNameSourcePropertyItemAttribute(Type type, string propertyName) : base(type)
        {
            this.PropertyName = propertyName;
        }

        public string PropertyName { get; set; }

        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            PropertyInfo p = obj.GetType().GetProperty(this.PropertyName);
            return p.GetValue(obj) as IEnumerable;
        }
    }
}
