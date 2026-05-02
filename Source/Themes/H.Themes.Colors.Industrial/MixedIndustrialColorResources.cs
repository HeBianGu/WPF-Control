// Copyright (c) HeBianGu Authors. All Rights Reserved.
// Author: HeBianGu
// Github: https://github.com/HeBianGu/WPF-Control
// Document: https://hebiangu.github.io/WPF-Control-Docs
// QQ:908293466 Group:971261058
// bilibili: https://space.bilibili.com/370266611
// Licensed under the MIT License (the "License")

using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Industrial;

[Display(Name = "石墨脉冲", GroupName = "混合配色", Description = "石墨黑、灰蓝、电蓝、橙色和绿色混合，适合专业后台和工业平台", Order = 160, Prompt = "混合")]
public class GraphitePulseColorResource : ResxColorResourceBase
{
    public GraphitePulseColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Industrial;component/GraphitePulse.xaml") };
}

[Display(Name = "指挥中心", GroupName = "混合配色", Description = "蓝黑、蓝色、青色、警示橙和红色混合，适合调度中心和生产大屏", Order = 161, Prompt = "混合")]
public class CommandCenterColorResource : ResxColorResourceBase
{
    public CommandCenterColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Industrial;component/CommandCenter.xaml") };
}
