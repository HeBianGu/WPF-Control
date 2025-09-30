// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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
