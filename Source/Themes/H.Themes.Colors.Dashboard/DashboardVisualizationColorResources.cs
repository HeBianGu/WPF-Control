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

[Display(Name = "海洋图表", GroupName = "数据可视化", Description = "适合图表、仪表盘、统计界面的海洋蓝主题", Order = 600, Prompt = "新增")]
public class ChartOceanColorResource : ResxColorResourceBase
{
    public ChartOceanColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Dashboard;component/ChartOcean.xaml") };
}

[Display(Name = "糖果图表", GroupName = "数据可视化", Description = "适合轻量统计和多彩图表的糖果色主题", Order = 601, Prompt = "新增")]
public class ChartCandyColorResource : ResxColorResourceBase
{
    public ChartCandyColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Dashboard;component/ChartCandy.xaml") };
}

[Display(Name = "科学图表", GroupName = "数据可视化", Description = "适合科研统计、实验数据和严谨图表的浅色主题", Order = 602, Prompt = "新增")]
public class ChartScientificColorResource : ResxColorResourceBase
{
    public ChartScientificColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Dashboard;component/ChartScientific.xaml") };
}

[Display(Name = "交通图表", GroupName = "数据可视化", Description = "适合交通流量、路线监控和信号统计的深色主题", Order = 603, Prompt = "新增")]
public class ChartTrafficColorResource : ResxColorResourceBase
{
    public ChartTrafficColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Dashboard;component/ChartTraffic.xaml") };
}

[Display(Name = "能源图表", GroupName = "数据可视化", Description = "适合能源、电网、功耗和趋势分析的深色主题", Order = 604, Prompt = "新增")]
public class ChartEnergyColorResource : ResxColorResourceBase
{
    public ChartEnergyColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Dashboard;component/ChartEnergy.xaml") };
}

[Display(Name = "热力图表", GroupName = "数据可视化", Description = "适合热力图、密度图和风险态势的深色主题", Order = 605, Prompt = "新增")]
public class ChartThermalColorResource : ResxColorResourceBase
{
    public ChartThermalColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Dashboard;component/ChartThermal.xaml") };
}

[Display(Name = "夜间仪表盘", GroupName = "数据可视化", Description = "适合监控大屏和夜间仪表盘的深色主题", Order = 606, Prompt = "新增")]
public class DashboardNightColorResource : ResxColorResourceBase
{
    public DashboardNightColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Dashboard;component/DashboardNight.xaml") };
}

[Display(Name = "监控绿色", GroupName = "数据可视化", Description = "适合监控、指标状态和运行日志的绿色深色主题", Order = 607, Prompt = "新增")]
public class MonitorGreenColorResource : ResxColorResourceBase
{
    public MonitorGreenColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Dashboard;component/MonitorGreen.xaml") };
}

[Display(Name = "指标蓝", GroupName = "数据可视化", Description = "适合 KPI、指标卡片和管理看板的蓝色浅色主题", Order = 608, Prompt = "新增")]
public class MetricBlueColorResource : ResxColorResourceBase
{
    public MetricBlueColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Dashboard;component/MetricBlue.xaml") };
}

[Display(Name = "分析紫", GroupName = "数据可视化", Description = "适合分析报表、统计洞察和趋势图的紫色浅色主题", Order = 609, Prompt = "新增")]
public class AnalyticsPurpleColorResource : ResxColorResourceBase
{
    public AnalyticsPurpleColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Dashboard;component/AnalyticsPurple.xaml") };
}
