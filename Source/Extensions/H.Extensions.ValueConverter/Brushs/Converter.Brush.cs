// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace H.Extensions.ValueConverter.Brushs;

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
