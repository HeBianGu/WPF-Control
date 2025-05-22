// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Globalization;

namespace H.ValueConverter.Brushs;

/// <summary> 绑定图标转换 </summary>
public class GetColorToBrushConverter : MarkupValueConverterBase
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;
        Color color = (Color)value;
        return new SolidColorBrush(color);
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        SolidColorBrush brush = value as SolidColorBrush;
        if (brush == null)
            return null;
        return brush.Color;
    }
}
