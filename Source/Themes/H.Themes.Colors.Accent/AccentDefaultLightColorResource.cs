// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Themes.Colors.Accent;

[Display(Name = "默认强调浅色", GroupName = "强调色", Description = "跟随基础浅色主题的默认强调配色", Order = 20, Prompt = "推荐")]
public class AccentDefaultLightColorResource : ResxColorResourceBase
{
    public AccentDefaultLightColorResource()
    {
        this.IsDark = false;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Accent;component/Light.xaml")
    };
}
