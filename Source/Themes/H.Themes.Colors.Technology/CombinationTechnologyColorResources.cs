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

[Display(Name = "极光电路", GroupName = "科技组合色", Description = "蓝紫青绿组合的极光电路深色主题", Order = 240, Prompt = "新增")]
public class AuroraCircuitDarkColorResource : ResxColorResourceBase
{
    public AuroraCircuitDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/AuroraCircuitDark.xaml") };
}

[Display(Name = "合成波网格", GroupName = "科技组合色", Description = "紫粉蓝组合的 Synthwave 深色主题", Order = 241, Prompt = "新增")]
public class SynthwaveGridDarkColorResource : ResxColorResourceBase
{
    public SynthwaveGridDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/SynthwaveGridDark.xaml") };
}

[Display(Name = "量子日落", GroupName = "科技组合色", Description = "紫橙玫红组合的量子日落深色主题", Order = 242, Prompt = "新增")]
public class QuantumSunsetDarkColorResource : ResxColorResourceBase
{
    public QuantumSunsetDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/QuantumSunsetDark.xaml") };
}

[Display(Name = "等离子风暴", GroupName = "科技组合色", Description = "蓝紫青组合的等离子风暴深色主题", Order = 243, Prompt = "新增")]
public class PlasmaStormDarkColorResource : ResxColorResourceBase
{
    public PlasmaStormDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/PlasmaStormDark.xaml") };
}

[Display(Name = "生物电路", GroupName = "科技组合色", Description = "绿色青色组合的生物科技深色主题", Order = 244, Prompt = "新增")]
public class BioCircuitDarkColorResource : ResxColorResourceBase
{
    public BioCircuitDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/BioCircuitDark.xaml") };
}

[Display(Name = "冰火核心", GroupName = "科技组合色", Description = "冷蓝与热橙对比的核心反应堆深色主题", Order = 245, Prompt = "新增")]
public class IceFireCoreDarkColorResource : ResxColorResourceBase
{
    public IceFireCoreDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/IceFireCoreDark.xaml") };
}

[Display(Name = "星云指挥", GroupName = "科技组合色", Description = "蓝紫星云与青色高亮的指挥舱深色主题", Order = 246, Prompt = "新增")]
public class NebulaOpsDarkColorResource : ResxColorResourceBase
{
    public NebulaOpsDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/NebulaOpsDark.xaml") };
}

[Display(Name = "太阳耀斑", GroupName = "科技组合色", Description = "橙红金组合的太阳耀斑深色主题", Order = 247, Prompt = "新增")]
public class SolarFlareDarkColorResource : ResxColorResourceBase
{
    public SolarFlareDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/SolarFlareDark.xaml") };
}

[Display(Name = "极光玻璃", GroupName = "科技组合色", Description = "蓝紫青组合的玻璃感浅色科技主题", Order = 260, Prompt = "新增")]
public class AuroraGlassLightColorResource : ResxColorResourceBase
{
    public AuroraGlassLightColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/AuroraGlassLight.xaml") };
}

[Display(Name = "霓虹粉彩", GroupName = "科技组合色", Description = "粉蓝紫组合的浅色霓虹主题", Order = 261, Prompt = "新增")]
public class NeonPastelLightColorResource : ResxColorResourceBase
{
    public NeonPastelLightColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/NeonPastelLight.xaml") };
}

[Display(Name = "量子实验室", GroupName = "科技组合色", Description = "蓝青紫组合的实验室浅色主题", Order = 262, Prompt = "新增")]
public class QuantumLabLightColorResource : ResxColorResourceBase
{
    public QuantumLabLightColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/QuantumLabLight.xaml") };
}

[Display(Name = "全息薄荷", GroupName = "科技组合色", Description = "薄荷青与蓝色组合的全息浅色主题", Order = 263, Prompt = "新增")]
public class HoloMintLightColorResource : ResxColorResourceBase
{
    public HoloMintLightColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/HoloMintLight.xaml") };
}
