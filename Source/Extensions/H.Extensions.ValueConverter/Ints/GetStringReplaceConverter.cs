// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Globalization;

namespace H.Extensions.ValueConverter.Ints;

/// <summary> 替换字符串 </summary>
public class GetStringReplaceConverter : MarkupValueConverterBase
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;
        if (parameter == null)
            return value;
        return value.ToString().Replace(value.ToString().Split(' ')[0], value.ToString().Split(' ')[1]);
    }
}
