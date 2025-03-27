// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Globalization;

namespace H.Extensions.ValueConverter.Doubles;

/// <summary> 百分比转换为角度值 </summary>
public class GetPercentToAngleConverter : MarkupValueConverterBase
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        double percent = double.Parse(value.ToString());
        if (percent >= 1) return 360.0D;
        return percent * 360;
    }
}
