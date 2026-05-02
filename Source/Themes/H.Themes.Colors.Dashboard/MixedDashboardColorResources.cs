// Copyright (c) HeBianGu Authors. All Rights Reserved.
// Author: HeBianGu
// Github: https://github.com/HeBianGu/WPF-Control
// Document: https://hebiangu.github.io/WPF-Control-Docs
// QQ:908293466 Group:971261058
// bilibili: https://space.bilibili.com/370266611
// Licensed under the MIT License (the "License")

using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Dashboard;

[Display(Name = "热力洞察", GroupName = "混合配色", Description = "橙色、红色、黄色与冷蓝混合，适合热力图、风险图和密度分析", Order = 140, Prompt = "混合")]
public class ThermalInsightColorResource : ResxColorResourceBase
{
    public ThermalInsightColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Dashboard;component/ThermalInsight.xaml") };
}
