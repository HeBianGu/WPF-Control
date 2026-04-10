// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Data;
using System.Windows.Markup;

namespace H.Modules.Globalization;

public class GlobalizationBinding : MarkupExtension
{
    public string Key { get; set; }
    public Type ResourcesType { get; set; }
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        Binding binding = new Binding(nameof(GlobalizationOptions.CultureInfo))
        {
            Source = GlobalizationOptions.Instance,
            Converter = new GetGlobalizationValueConverter
            {
                ResourcesType = this.ResourcesType,
                Key = Key
            }
        };
        binding.IsAsync = true;
        return binding.ProvideValue(serviceProvider);
    }
}
