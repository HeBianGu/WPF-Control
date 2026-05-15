// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.Vision.Geometrys;

public class VisionLineTypeConverter : TypeConverter
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
                double.TryParse(parts[0], out double sx) &&
                double.TryParse(parts[1], out double sy) &&
                double.TryParse(parts[2], out double ex) &&
                double.TryParse(parts[3], out double ey))
            {
                return new VisionLine(new Point(sx, sy), new Point(ex, ey));
            }
        }
        throw new ArgumentException("Invalid format. Expected format: 'sx,sy,ex,ey'");
    }

    public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is VisionLine line)
            return $"{line.Start.X},{line.Start.Y},{line.End.X},{line.End.Y}";

        return base.ConvertTo(context, culture, value, destinationType);
    }
}