// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagrams.OpenCV.TypeConverters;

/// <summary>  
/// Converts Scalar objects to and from other representations.  
/// </summary>  
public class ScalarTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object value)
    {
        if (value is string stringValue)
        {
            string[] parts = stringValue.Split(',');
            if (parts.Length == 4 &&
                double.TryParse(parts[0], out double v0) &&
                double.TryParse(parts[1], out double v1) &&
                double.TryParse(parts[2], out double v2) &&
                double.TryParse(parts[3], out double v3))
            {
                return new Scalar(v0, v1, v2, v3);
            }
        }

        return base.ConvertFrom(context, culture, value);
    }

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, System.Globalization.CultureInfo? culture, object? value, Type destinationType)
    {
        var result = value is Scalar scalar && destinationType == typeof(string)
            ? $"{scalar.Val0},{scalar.Val1},{scalar.Val2},{scalar.Val3}"
            : base.ConvertTo(context, culture, value, destinationType);
        return result;
    }
}
