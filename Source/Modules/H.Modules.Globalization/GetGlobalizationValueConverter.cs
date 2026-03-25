// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.ValueConverter;
using System.Globalization;
using System.Resources;

namespace H.Modules.Globalization;

public class GetGlobalizationValueConverter : MarkupValueConverterBase
{
    public string Key { get; set; }
    public Type ResourcesType { get; set; }
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (this.ResourcesType == null)
            return DependencyProperty.UnsetValue;
        var name = this.ResourcesType.FullName;
        var rm = new ResourceManager(name, this.ResourcesType.Assembly);
        return rm.GetString(this.Key);
    }
}
