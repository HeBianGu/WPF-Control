// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.ValueConverter.Visiblilitys;

public class GetEqualsToVisibilityStringConverter : MarkupValueConverterBase
{
    public Visibility Visibility { get; set; } = Visibility.Visible;
    public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value != null && parameter != null)
        {
            if (value.Equals(parameter))
                return this.Visibility;
        }
        return this.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

    }
}
