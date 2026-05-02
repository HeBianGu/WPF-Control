// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Technology;

/// <summary>
/// 星际无线靛：深邃而广阔，如同无垠的宇宙，充满了未知与探索。
/// </summary>
[Display(Name = "科技靛蓝", GroupName = "科技未来", Description = "科技靛蓝科技未来主题", Order = 416, Prompt = "新增")]
public class TechnologyIndigoColorResource : ResxColorResourceBase
{
    public TechnologyIndigoColorResource()
    {
        this.IsDark = true;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/TechnologyIndigo.xaml")
    };
}
