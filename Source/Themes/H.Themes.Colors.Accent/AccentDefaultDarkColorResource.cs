// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Themes.Colors.Accent;

[Display(Name = "默认强调深色", GroupName = "强调色", Description = "跟随基础深色主题的默认强调配色", Order = 60, Prompt = "长期支持")]
public class AccentDefaultDarkColorResource : ResxColorResourceBase
{
    public AccentDefaultDarkColorResource()
    {
        this.IsDark = true;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Accent;component/Dark.xaml")
    };
}
