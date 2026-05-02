// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Platform;
[Display(Name = "Fluent UI 风格", GroupName = "平台风格", Description = "Fluent UI 系统平台风格主题", Order = 712, Prompt = "新增")]
public class FluentUIColorResource : ResxColorResourceBase
{
    public FluentUIColorResource()
    {
        this.IsDark = false;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Platform;component/FluentUI.xaml")
    };
}
