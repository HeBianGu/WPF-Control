﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Services.Common;
using System;
using System.Collections;
using System.Reflection;

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
                return new StringFilter(property, source);
            }
            else if (property.PropertyType.FullName == typeof(int).FullName)
            {
                return new IntFilter(property);
            }
            else if (property.PropertyType.FullName == typeof(long).FullName)
            {
                return new LongFilter(property);
            }
            else if (property.PropertyType.FullName == typeof(double).FullName)
            {
                return new DoubleFilter(property);
            }
            else if (property.PropertyType.FullName == typeof(bool).FullName)
            {
                return new BooleanFilter(property);
            }
            else if (property.PropertyType.FullName == typeof(DateTime).FullName)
            {
                return new DateTimeFilter(property);
            }

            throw new NotImplementedException($"{property.PropertyType.FullName} 类型没有实现，请先实现该类型方法");
        }
    }
}
