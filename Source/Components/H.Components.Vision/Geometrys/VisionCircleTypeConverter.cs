// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.Vision.Geometrys;

public class VisionCircleTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
    {
        if (value is string stringValue)
        {
            var parts = stringValue.Split(',');
            if (parts.Length == 3 &&
                double.TryParse(parts[0], out double row) &&
                double.TryParse(parts[1], out double column) &&
                double.TryParse(parts[2], out double radius))
            {
                return new VisionCircle(new Point(row, column), radius);
            }
        }

        throw new ArgumentException("Invalid format. Expected format: 'row,column,radius'");
    }

    public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is VisionCircle circle)
            return $"{circle.Point.X},{circle.Point.Y},{circle.Radius}";

        return base.ConvertTo(context, culture, value, destinationType);
    }
}