// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Globalization;

namespace H.ValueConverter.Brushs;

[Obsolete]
public class BrushRoundConverter : IValueConverter
{
    public Brush HighValue { get; set; } = Brushes.White;

    public Brush LowValue { get; set; } = Brushes.Black;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        SolidColorBrush solidColorBrush = value as SolidColorBrush;
        if (solidColorBrush == null)
            return null;
        Color color = solidColorBrush.Color;
        double brightness = 0.3 * color.R + 0.59 * color.G + 0.11 * color.B;
        return brightness < 123 ? this.LowValue : this.HighValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Binding.DoNothing;
    }
}
