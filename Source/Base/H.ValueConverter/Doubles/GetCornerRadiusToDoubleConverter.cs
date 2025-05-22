// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Globalization;

namespace H.ValueConverter.Doubles;

public class GetCornerRadiusToDoubleConverter : MarkupValueConverterBase
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter != null)
            return ((CornerRadius)value).TopLeft * System.Convert.ToDouble(parameter);
        return ((CornerRadius)value).TopLeft;
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter != null)
            return new CornerRadius((double)value / System.Convert.ToDouble(parameter));
        return new CornerRadius((double)value);
    }
}
