// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Globalization;

namespace H.ValueConverter.Brushs;

/// <summary> 根据背景色获取前景色。当然也可反着用 </summary>
public class GetBackgroundToForegroundConverter : MarkupValueConverterBase
{
    private Color IdealTextColor(Color bg)
    {
        const int nThreshold = 105;
        int bgDelta = System.Convert.ToInt32(bg.R * 0.299 + bg.G * 0.587 + bg.B * 0.114);
        Color foreColor = 255 - bgDelta < nThreshold ? Colors.Black : Colors.White;
        return foreColor;
    }

    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is SolidColorBrush)
        {
            Color idealForegroundColor = IdealTextColor(((SolidColorBrush)value).Color);
            SolidColorBrush foreGroundBrush = new SolidColorBrush(idealForegroundColor);
            foreGroundBrush.Freeze();
            return foreGroundBrush;
        }
        return Brushes.White;
    }
}
