// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Globalization;

namespace H.ValueConverter.Types;

public class GetTypeAttributeConverter : MarkupValueConverterBase
{
    public Type AttributeType { get; set; } = typeof(DescriptionAttribute);
    public int Index { get; set; } = 0;
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;
        Type type = value is Type t ? t : value.GetType();
        object[] attributes = type.GetCustomAttributes(this.AttributeType, false);
        if (attributes == null || attributes.Count() == 0)
            return DependencyProperty.UnsetValue;
        return attributes[0];

    }
}
