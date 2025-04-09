// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Globalization;
using System.Windows;

namespace H.Extensions.ValueConverter.Doubles;

public class GetCornerRadiusToDoubleConverter : MarkupValueConverterBase
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter != null)
            return ((CornerRadius)value).TopLeft * System.Convert.ToDouble(parameter);
        return ((CornerRadius)value).TopLeft;
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter != null)
            return new CornerRadius((double)value / System.Convert.ToDouble(parameter));
        return new CornerRadius((double)value);
    }
}
