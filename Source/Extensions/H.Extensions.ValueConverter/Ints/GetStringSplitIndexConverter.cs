// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Globalization;

namespace H.Extensions.ValueConverter.Ints;

/// <summary> 分割字符串取第一个值 </summary>
public class GetStringSplitIndexConverter : MarkupValueConverterBase
{
    public int Index { get; set; } = 0;
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;
        if (parameter == null)
            return value.ToString().Split(' ')[this.Index];
        return value.ToString().Split(parameter.ToString().ToCharArray())[this.Index];
    }
}
