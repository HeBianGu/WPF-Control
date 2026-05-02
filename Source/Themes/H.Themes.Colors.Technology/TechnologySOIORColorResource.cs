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
/// SOIOR：清新而自然，如同春天的气息，充满了生机与活力。
/// </summary>
[Display(Name = "科技深邃蓝", GroupName = "科技未来", Description = "科技深邃蓝科技未来主题", Order = 417, Prompt = "新增")]
public class TechnologySOIORColorResource : ResxColorResourceBase
{
    public TechnologySOIORColorResource()
    {
        this.IsDark = false;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/TechnologySOIOR.xaml")
    };
}
