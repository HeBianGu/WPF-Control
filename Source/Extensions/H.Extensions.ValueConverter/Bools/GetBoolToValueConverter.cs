﻿// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Globalization;

namespace H.Extensions.ValueConverter.Bools;

public class GetBoolToValueConverter : MarkupValueConverterBase
{
    public object TrueValue { get; set; }
    public object FalseValue { get; set; }
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b)
            return b ? this.TrueValue : this.FalseValue;
        return this.DefaultValue;
    }
}
