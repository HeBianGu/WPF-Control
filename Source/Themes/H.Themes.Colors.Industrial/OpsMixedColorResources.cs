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

[Display(Name = "产线脉冲", GroupName = "工业混合配色", Description = "深灰黑、安全橙、绿色、蓝色混合，适合生产线状态和设备监控", Order = 180, Prompt = "混合")]
public class ProductionLineColorResource : ResxColorResourceBase
{
    public ProductionLineColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Industrial;component/ProductionLine.xaml") };
}

[Display(Name = "告警看板", GroupName = "工业混合配色", Description = "深红黑、红色、琥珀、白色混合，适合异常报警和风险看板", Order = 181, Prompt = "混合")]
public class AlarmBoardColorResource : ResxColorResourceBase
{
    public AlarmBoardColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Industrial;component/AlarmBoard.xaml") };
}

[Display(Name = "质量关卡", GroupName = "工业混合配色", Description = "深蓝灰、绿色、黄色、红色混合，适合质检、良率统计和质量评估", Order = 182, Prompt = "混合")]
public class QualityGateColorResource : ResxColorResourceBase
{
    public QualityGateColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Industrial;component/QualityGate.xaml") };
}

[Display(Name = "运维雷达", GroupName = "工业混合配色", Description = "深蓝黑、雷达绿、青色、黄色混合，适合系统运行监控和资源监控", Order = 183, Prompt = "混合")]
public class OpsRadarColorResource : ResxColorResourceBase
{
    public OpsRadarColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Industrial;component/OpsRadar.xaml") };
}
