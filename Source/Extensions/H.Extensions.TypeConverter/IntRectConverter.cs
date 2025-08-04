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
    public class IntRectConverter : System.ComponentModel.TypeConverter
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
                string[] parts = strValue.Split(',');
                if (parts.Length == 4)
                {
                    if (int.TryParse(parts[0], out int x) &&
                        int.TryParse(parts[1], out int y) &&
                        int.TryParse(parts[2], out int width) &&
                        int.TryParse(parts[3], out int height))
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
                // 将 double 转为 int
                int x = (int)Math.Round(rect.X);
                int y = (int)Math.Round(rect.Y);
                int width = (int)Math.Round(rect.Width);
                int height = (int)Math.Round(rect.Height);

                return $"{x},{y},{width},{height}";
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
