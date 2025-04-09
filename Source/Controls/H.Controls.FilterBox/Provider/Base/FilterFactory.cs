// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using System.Reflection;

namespace H.Controls.FilterBox
{

    public static class FilterFactory
    {
        public static IFilterable Create(PropertyInfo property, IEnumerable source)
        {
            if (property == null) return null;

            if (!property.CanRead) return null;

            if (property.PropertyType.FullName == typeof(string).FullName)
            {
                return new StringPropertyFilter(property, source);
            }
            else if (property.PropertyType.FullName == typeof(int).FullName)
            {
                return new IntPropertyFilter(property);
            }
            else if (property.PropertyType.FullName == typeof(long).FullName)
            {
                return new LongPropertyFilter(property);
            }
            else if (property.PropertyType.FullName == typeof(double).FullName)
            {
                return new DoublePropertyFilter(property);
            }
            else if (property.PropertyType.FullName == typeof(bool).FullName)
            {
                return new BooleanPropertyFilter(property);
            }
            else if (property.PropertyType.FullName == typeof(DateTime).FullName)
            {
                return new DateTimePropertyFilter(property);
            }

            throw new NotImplementedException($"{property.PropertyType.FullName} 类型没有实现，请先实现该类型方法");
        }
    }
}
