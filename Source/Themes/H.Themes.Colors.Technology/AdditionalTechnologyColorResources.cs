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

[Display(Name = "霓虹青", GroupName = "科技深色", Description = "高亮霓虹青科技主题", Order = 200, Prompt = "新增")]
public class NeonCyanDarkColorResource : ResxColorResourceBase
{
    public NeonCyanDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/NeonCyanDark.xaml") };
}

[Display(Name = "量子紫", GroupName = "科技深色", Description = "量子紫色科技主题", Order = 201, Prompt = "新增")]
public class QuantumPurpleDarkColorResource : ResxColorResourceBase
{
    public QuantumPurpleDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/QuantumPurpleDark.xaml") };
}

[Display(Name = "矩阵绿", GroupName = "科技深色", Description = "矩阵终端绿色科技主题", Order = 202, Prompt = "新增")]
public class MatrixGreenDarkColorResource : ResxColorResourceBase
{
    public MatrixGreenDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/MatrixGreenDark.xaml") };
}

[Display(Name = "全息蓝", GroupName = "科技深色", Description = "全息蓝色科技主题", Order = 203, Prompt = "新增")]
public class HologramBlueDarkColorResource : ResxColorResourceBase
{
    public HologramBlueDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/HologramBlueDark.xaml") };
}

[Display(Name = "星舰橙", GroupName = "科技深色", Description = "星舰仪表橙色科技主题", Order = 204, Prompt = "新增")]
public class StarshipOrangeDarkColorResource : ResxColorResourceBase
{
    public StarshipOrangeDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/StarshipOrangeDark.xaml") };
}

[Display(Name = "AI 品红", GroupName = "科技深色", Description = "AI 品红强调科技主题", Order = 205, Prompt = "新增")]
public class AIMagentaDarkColorResource : ResxColorResourceBase
{
    public AIMagentaDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/AIMagentaDark.xaml") };
}

[Display(Name = "数据中心", GroupName = "科技深色", Description = "数据中心蓝灰科技主题", Order = 206, Prompt = "新增")]
public class DataCenterDarkColorResource : ResxColorResourceBase
{
    public DataCenterDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/DataCenterDark.xaml") };
}

[Display(Name = "激光红", GroupName = "科技深色", Description = "激光红警戒科技主题", Order = 207, Prompt = "新增")]
public class LaserRedDarkColorResource : ResxColorResourceBase
{
    public LaserRedDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/LaserRedDark.xaml") };
}

[Display(Name = "实验室浅蓝", GroupName = "科技浅色", Description = "实验室风格浅色科技主题", Order = 220, Prompt = "新增")]
public class LabBlueLightColorResource : ResxColorResourceBase
{
    public LabBlueLightColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/LabBlueLight.xaml") };
}

[Display(Name = "未来白", GroupName = "科技浅色", Description = "未来白极简科技主题", Order = 221, Prompt = "新增")]
public class FutureWhiteLightColorResource : ResxColorResourceBase
{
    public FutureWhiteLightColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/FutureWhiteLight.xaml") };
}

[Display(Name = "电路青", GroupName = "科技浅色", Description = "电路青浅色科技主题", Order = 222, Prompt = "新增")]
public class CircuitCyanLightColorResource : ResxColorResourceBase
{
    public CircuitCyanLightColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/CircuitCyanLight.xaml") };
}

[Display(Name = "机器人灰", GroupName = "科技浅色", Description = "机器人灰浅色科技主题", Order = 223, Prompt = "新增")]
public class RobotGrayLightColorResource : ResxColorResourceBase
{
    public RobotGrayLightColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/RobotGrayLight.xaml") };
}
