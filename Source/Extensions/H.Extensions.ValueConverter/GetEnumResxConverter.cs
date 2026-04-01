// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common;
using System.Reflection;

namespace H.Extensions.ValueConverter;

public class GetEnumResxConverter : MarkupValueConverterBase
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Enum e)
        {
            var type = value.GetType();
            FieldInfo fi = value.GetType().GetField(value.ToString());
            string def = fi.GetCustomAttribute<DisplayAttribute>()?.Name;
            var result = e.GetEnumNameResx(def) ?? def;
            return result;
        }
        return DependencyProperty.UnsetValue;
    }
}
