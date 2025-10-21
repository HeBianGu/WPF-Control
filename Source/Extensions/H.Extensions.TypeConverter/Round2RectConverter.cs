// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.ComponentModel;
using System.Globalization;
using System.Windows;

namespace H.Extensions.TypeConverter
{
    public class Round2RectConverter : System.ComponentModel.TypeConverter
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
                    return Rect.Empty;
                string[] parts = strValue.Split(',');
                if (parts.Length == 4)
                {
                    if (double.TryParse(parts[0], out double x) &&
                        double.TryParse(parts[1], out double y) &&
                        double.TryParse(parts[2], out double width) &&
                        double.TryParse(parts[3], out double height))
                    {
                        return new Rect(x, y, width, height);
                    }
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is Rect rect)
            {
                if (rect.IsEmpty)
                    return "";
                // 将 double 转为 int
                double x = Math.Round(rect.X, 2);
                double y = Math.Round(rect.Y, 2);
                double width = Math.Round(rect.Width, 2);
                double height = Math.Round(rect.Height, 2);
                return $"{x},{y},{width},{height}";
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
