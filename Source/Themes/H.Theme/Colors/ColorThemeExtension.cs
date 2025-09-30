// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Themes.Colors;

public class ColorThemeExtension : MarkupExtension
{
    public ColorThemeType Type { get; set; }
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (this.Type == ColorThemeType.Default)
            return new DefaultColorResource().Resource;
        if (this.Type == ColorThemeType.Dark)
            return new DarkColorResource().Resource;
        if (this.Type == ColorThemeType.Light)
            return new LightColorResource().Resource;
        return null;
    }
}
