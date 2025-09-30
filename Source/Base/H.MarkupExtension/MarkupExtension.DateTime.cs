// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Markup;

namespace H.MarkupExtension;

[MarkupExtensionReturnType(typeof(DateTime))]
public class GetDateTimeExtension : GetValueExtensionBase
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (DateTime.TryParse(this.Value, out DateTime result))
        {
            return result;
        }
        return DateTime.MinValue;
    }
}
