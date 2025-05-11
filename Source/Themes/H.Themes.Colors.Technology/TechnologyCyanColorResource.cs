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
/// 电骼兽藏青：深邃而神秘，如同夜晚的星空，引人深思。
/// </summary>
[Display(Name = "电骼兽藏青", GroupName = "深色科技风", Description = "深邃而神秘，如同夜晚的星空，引人深思", Order = 100, Prompt = "试验")]
public class TechnologyCyanColorResource : ColorResourceBase
{
    public TechnologyCyanColorResource()
    {
        this.IsDark = true;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/TechnologyCyan.xaml")
    };
}
