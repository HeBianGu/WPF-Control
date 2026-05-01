// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Markup;

namespace H.Themes.Colors.Accent;

public class AccentBlueLightThemeExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new AccentBlueLightColorResource().Resource;
    }
}

public class AccentBlueDarkThemeExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return new AccentBlueDarkColorResource().Resource;
    }
}

public class AccentLightThemeExtension : AccentBlueLightThemeExtension
{
}

public class AccentDarkThemeExtension : AccentBlueDarkThemeExtension
{
}
