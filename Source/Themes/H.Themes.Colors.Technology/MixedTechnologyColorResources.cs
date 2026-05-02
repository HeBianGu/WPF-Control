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

[Display(Name = "极光运维", GroupName = "混合配色", Description = "青蓝、紫色、荧光绿混合，适合监控大屏和运维平台", Order = 330, Prompt = "混合")]
public class AuroraOpsColorResource : ResxColorResourceBase
{
    public AuroraOpsColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/AuroraOps.xaml") };
}

[Display(Name = "霓虹矩阵", GroupName = "混合配色", Description = "矩阵绿、青色、紫粉混合，适合 AI、算法和日志控制台", Order = 331, Prompt = "混合")]
public class NeonMatrixColorResource : ResxColorResourceBase
{
    public NeonMatrixColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/NeonMatrix.xaml") };
}

[Display(Name = "赛博日落", GroupName = "混合配色", Description = "玫红、橙色、紫色和青蓝混合，适合科技展示", Order = 332, Prompt = "混合")]
public class CyberSunsetColorResource : ResxColorResourceBase
{
    public CyberSunsetColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/CyberSunset.xaml") };
}

[Display(Name = "海洋信号", GroupName = "混合配色", Description = "湖蓝、蓝色、薄荷绿和黄色混合，适合数据链路和网络监控", Order = 333, Prompt = "混合")]
public class OceanSignalColorResource : ResxColorResourceBase
{
    public OceanSignalColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/OceanSignal.xaml") };
}

[Display(Name = "生物科技实验室", GroupName = "混合配色", Description = "医疗蓝、薄荷绿、紫色混合，适合医疗、实验室和 AI 视觉", Order = 334, Prompt = "混合")]
public class BioTechLabColorResource : ResxColorResourceBase
{
    public BioTechLabColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/BioTechLab.xaml") };
}
