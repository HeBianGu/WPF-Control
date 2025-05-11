// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Markup;

namespace H.Extensions.FontIcon;

public class GetIconSegoesExtension : MarkupExtension
{
    public int From { get; set; } = 0xE700;
    public int To { get; set; } = 0xE900;
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var ds = IconSegoeProvider.GetIconSegoes(this.From, this.To);
        return new IconSegoes(ds);
    }
}
