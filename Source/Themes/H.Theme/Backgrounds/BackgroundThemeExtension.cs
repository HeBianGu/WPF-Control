// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Themes.Backgrounds;

public class BackgroundThemeExtension : MarkupExtension
{
    public BackgroundThemeType Type { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        if (this.Type == BackgroundThemeType.LinearGradientBrush)
            return new LinearGradientBrushResource().Resource;
        return new SolidColorBrushResource().Resource;
    }
}
