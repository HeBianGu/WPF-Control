// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.ComponentModel;
using System.Globalization;

namespace H.Extensions.TypeConverter
{
    public class NullableInt16Converter : Int16Converter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is null)
                return null;
            if (value is string s)
            {
                if (string.IsNullOrWhiteSpace(s))
                    return null;
                // Delegate to base for parsing with culture-awareness
                return base.ConvertFrom(context, culture, s);
            }
            return base.ConvertFrom(context, culture, value);
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;
            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value is null)
                    return string.Empty;
                if (value is short s)
                    return s.ToString(culture ?? CultureInfo.CurrentCulture);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
