// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Collections;

namespace H.ValueConverter.Visiblilitys;

public class GetItemInListToVisibilityConverter : MarkupValueConverterBase
{
    public Visibility Visibility { get; set; } = Visibility.Visible;
    public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (parameter is IList list)
        {
            if (list.Contains(value))
                return this.Visibility;
        }
        return this.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
    }
}
