// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Globalization;

namespace H.Extensions.ValueConverter.Ints;

/// <summary> 设置文本框PropertyChanged 可以输入小数点 </summary>
public class GetDoubleTextConverter : MarkupValueConverterBase
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string str = value.ToString();
        if (str.EndsWith("."))
            return ".";
        if (str.Contains(".") && str.EndsWith("0"))
            return ".";
        return value;
    }
}
