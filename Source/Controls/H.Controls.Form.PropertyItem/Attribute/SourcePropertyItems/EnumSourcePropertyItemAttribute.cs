// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Collections;
using System.Reflection;

namespace H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem
{
    public class EnumSourcePropertyItemAttribute : SourcePropertyItemBaseAttribute
    {
        public EnumSourcePropertyItemAttribute(Type type) : base(type)
        {

        }


        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            if (propertyInfo.PropertyType.IsEnum == false)
                return null;
            Array result = propertyInfo.PropertyType.GetEnumValues();
            return result;
        }
    }
}
