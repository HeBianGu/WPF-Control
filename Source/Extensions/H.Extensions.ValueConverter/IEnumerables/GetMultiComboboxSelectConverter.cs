// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Globalization;

namespace H.Extensions.ValueConverter.IEnumerables;

/// <summary>
/// 区间范围筛选
/// </summary>
public class GetMultiComboboxSelectConverter : MarkupMultiValueConverterBase
{
    public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values == null) 
            return null;
        if (values.Length != 2) 
            return null;
        if (values[1] == null) 
            return null;

        int selectIndex = (int)values[0];
        IEnumerable<object> enumerable = values[1] as IEnumerable<object>;
        return enumerable.Skip(selectIndex).ToList();
    }
}
