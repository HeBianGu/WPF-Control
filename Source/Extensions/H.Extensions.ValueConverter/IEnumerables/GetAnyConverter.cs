// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Globalization;

namespace H.Extensions.ValueConverter.IEnumerables;

/// <summary>
/// 或运算
/// </summary>
public class GetAnyConverter : MarkupMultiValueConverterBase
{
    public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values == null) return null;

        List<bool> result = values.OfType<bool>()?.ToList();

        return result.Any(l => l == true);
    }
}
