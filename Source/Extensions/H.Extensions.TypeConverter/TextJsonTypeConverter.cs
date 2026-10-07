// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.ComponentModel;
using System.Globalization;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace H.Extensions.TypeConverter
{
    public class TextJsonTypeConverter<T> : System.ComponentModel.TypeConverter
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            PropertyNameCaseInsensitive = true,
        };

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            => sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            => destinationType == typeof(string) || base.CanConvertTo(context, destinationType);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string strValue)
            {
                if (string.IsNullOrEmpty(strValue))
                    return default(T);
                try
                {
                    return JsonSerializer.Deserialize<T>(strValue, Options);
                }
                catch (JsonException ex)
                {
                    throw new NotSupportedException($"无法将字符串转换为 {typeof(T)}", ex);
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture,
            object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value is null) return string.Empty;
                return JsonSerializer.Serialize(value, typeof(T), Options);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
