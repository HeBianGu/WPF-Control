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

    public class NullableFloatConverter : System.ComponentModel.SingleConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string) || sourceType == typeof(float) || sourceType == typeof(float?))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string) || destinationType == typeof(float) || destinationType == typeof(float?))
                return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is null)
                return (float?)null;

            if (value is string s)
            {
                if (string.IsNullOrWhiteSpace(s))
                    return (float?)null;

                var format = (culture ?? CultureInfo.CurrentCulture).NumberFormat;
                if (float.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, format, out var result))
                    return (float?)result;

                throw new FormatException($"Input string '{s}' was not in a correct format for a nullable float.");
            }

            if (value is float f)
                return (float?)f;

            if (value is double d)
                return (float?)Convert.ToSingle(d);

            return base.ConvertFrom(context, culture, value);
        }

        public override object? ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value is null)
                    return string.Empty;

                if (value is float f)
                    return f.ToString(culture ?? CultureInfo.CurrentCulture);

                // Fix for CS8116: check for boxed nullable float
                if (value != null && value.GetType() == typeof(float?))
                {
                    float? nf = (float?)value;
                    return nf.HasValue ? nf.Value.ToString(culture ?? CultureInfo.CurrentCulture) : string.Empty;
                }
            }

            if (destinationType == typeof(float))
            {
                if (value is float f)
                    return f;

                // Fix for CS8116: check for boxed nullable float
                if (value != null && value.GetType() == typeof(float?))
                {
                    float? nf = (float?)value;
                    return nf.GetValueOrDefault();
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
