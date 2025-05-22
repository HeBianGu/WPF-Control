// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Globalization;

namespace H.ValueConverter.Ints;

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
