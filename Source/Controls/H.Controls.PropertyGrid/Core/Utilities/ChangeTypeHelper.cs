// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.ComponentModel;

namespace H.Controls.PropertyGrid
{
    internal static class ChangeTypeHelper
    {
        internal static object ChangeType(object value, Type conversionType, IFormatProvider provider)
        {
            if (conversionType == null)
            {
                throw new ArgumentNullException("conversionType");
            }
            if (conversionType == typeof(Guid))
            {
                return new Guid(value.ToString());
            }
            else if (conversionType == typeof(Guid?))
            {
                if (value == null)
                    return null;
                return new Guid(value.ToString());
            }
            else if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                    return null;
                NullableConverter nullableConverter = new NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }

            return System.Convert.ChangeType(value, conversionType, provider);
        }
    }
}
