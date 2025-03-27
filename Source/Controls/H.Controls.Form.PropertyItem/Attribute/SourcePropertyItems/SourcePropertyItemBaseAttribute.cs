// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Controls.Form.Attributes;
using System.Collections;
using System.Reflection;

namespace H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem
{
    public abstract class SourcePropertyItemBaseAttribute : PropertyItemAttribute
    {
        public SourcePropertyItemBaseAttribute(Type type) : base(type)
        {

        }

        public abstract IEnumerable GetSource(PropertyInfo propertyInfo, object obj);
    }
}
