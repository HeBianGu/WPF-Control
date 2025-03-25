// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Globalization;
using System.Windows.Media;

namespace H.Extensions.ValueConverter.Brushs;

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
