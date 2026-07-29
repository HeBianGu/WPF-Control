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
    public class Int32SizeConverter : System.ComponentModel.TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string strValue)
            {
                if (string.IsNullOrEmpty(strValue))
                    return System.Windows.Size.Empty;
                string[] parts = strValue.Split(',');
                if (parts.Length == 2)
                {
                    if (int.TryParse(parts[0], out int x) &&
                        int.TryParse(parts[1], out int y))
                    {
                        return new System.Windows.Size((int)x, (int)y);
                    }
                    else
                        throw new ArgumentException("不是有效整型参数");
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is System.Windows.Size size)
            {
                if (size.IsEmpty)
                    return "";
                return $"{(int)size.Width},{(int)size.Height}";
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
