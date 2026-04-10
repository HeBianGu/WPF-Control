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
    public class NullableDoubleConverter : DoubleConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
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
                s = s.Trim();

                if (string.IsNullOrEmpty(s))
                    return null;

                // Try using base converter with provided culture
                try
                {
                    return base.ConvertFrom(context, culture, s);
                }
                catch
                {
                    // Fallback: try invariant culture
                    try
                    {
                        return base.ConvertFrom(context, CultureInfo.InvariantCulture, s);
                    }
                    catch
                    {
                        // Final attempt: parse manually
                        if (double.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, culture ?? CultureInfo.CurrentCulture, out var d))
                            return d;
                        if (double.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out d))
                            return d;

                        throw new FormatException($"Input string '{s}' was not in a correct format for a nullable double.");
                    }
                }
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

                // Delegate to base for consistent formatting
                try
                {
                    return base.ConvertTo(context, culture, value, destinationType);
                }
                catch
                {
                    var d = (double)value;
                    return d.ToString(culture ?? CultureInfo.CurrentCulture);
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
