// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Globalization;

namespace H.ValueConverter.IEnumerables;

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
