// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Globalization;
using System.Windows;

namespace H.Extensions.ValueConverter.Doubles;

public class GetThicknessToDoubleConverter : MarkupValueConverterBase
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Thickness thickness = (Thickness)value;
        return thickness.Left;
    }
}
