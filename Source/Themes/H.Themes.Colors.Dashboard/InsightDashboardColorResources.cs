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

[Display(Name = "洞察棱镜", GroupName = "洞察看板", Description = "深蓝紫、紫色、青色、粉色混合，适合综合看板和趋势洞察", Order = 160, Prompt = "混合")]
public class InsightPrismColorResource : ResxColorResourceBase
{
    public InsightPrismColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Dashboard;component/InsightPrism.xaml") };
}

[Display(Name = "报表画布", GroupName = "洞察看板", Description = "浅白灰、蓝色、紫色、琥珀混合，适合管理报表和统计分析", Order = 161, Prompt = "混合")]
public class ReportCanvasColorResource : ResxColorResourceBase
{
    public ReportCanvasColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Dashboard;component/ReportCanvas.xaml") };
}

[Display(Name = "指标极光", GroupName = "洞察看板", Description = "深蓝黑、青色、绿色、紫色混合，适合 KPI、指标卡片和监控大屏", Order = 162, Prompt = "混合")]
public class MetricAuroraColorResource : ResxColorResourceBase
{
    public MetricAuroraColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Dashboard;component/MetricAurora.xaml") };
}

[Display(Name = "风险热图", GroupName = "洞察看板", Description = "深棕黑、红色、橙色、黄色、蓝色混合，适合风险热力图和异常分布", Order = 163, Prompt = "混合")]
public class RiskHeatmapColorResource : ResxColorResourceBase
{
    public RiskHeatmapColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Dashboard;component/RiskHeatmap.xaml") };
}
