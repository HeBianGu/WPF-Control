// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows;
using System.Windows.Markup;

namespace H.MarkupExtension;

[MarkupExtensionReturnType(typeof(Visibility))]
public class GetVisibilityExtension : GetValueExtensionBase
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (Enum.TryParse<Visibility>(this.Value, out Visibility result))
        {
            return result;
        }
        return Visibility.Visible;
    }
}
