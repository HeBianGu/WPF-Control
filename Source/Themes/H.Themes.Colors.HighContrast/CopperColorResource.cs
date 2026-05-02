// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.HighContrast;
[Display(Name = "深铜色", GroupName = "推荐主题", Description = "深铜色推荐主题", Order = 100, Prompt = "推荐")]
public class HighContrastColorResource : ResxColorResourceBase
{
    public HighContrastColorResource()
    {
        this.IsDark = true;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.HighContrast;component/Dark.xaml")
    };
}
