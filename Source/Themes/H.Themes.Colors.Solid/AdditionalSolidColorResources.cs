// Copyright (c) HeBianGu Authors. All Rights Reserved.
// Author: HeBianGu
// Github: https://github.com/HeBianGu/WPF-Control
// Document: https://hebiangu.github.io/WPF-Control-Docs
// QQ:908293466 Group:971261058
// bilibili: https://space.bilibili.com/370266611
// Licensed under the MIT License (the "License")

using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Solid;

[Display(Name = "北欧夜", GroupName = "纯色深色", Description = "Nord 风格的冷调深色主题", Order = 150, Prompt = "新增")]
public class NordDarkColorResource : ResxColorResourceBase
{
    public NordDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/NordDark.xaml") };
}

[Display(Name = "德古拉", GroupName = "纯色深色", Description = "高对比紫粉色深色主题", Order = 151, Prompt = "新增")]
public class DraculaDarkColorResource : ResxColorResourceBase
{
    public DraculaDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/DraculaDark.xaml") };
}

[Display(Name = "海洋深渊", GroupName = "纯色深色", Description = "深海蓝绿风格主题", Order = 152, Prompt = "新增")]
public class OceanAbyssDarkColorResource : ResxColorResourceBase
{
    public OceanAbyssDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/OceanAbyssDark.xaml") };
}

[Display(Name = "酒红夜", GroupName = "纯色深色", Description = "酒红色商务深色主题", Order = 153, Prompt = "新增")]
public class WineDarkColorResource : ResxColorResourceBase
{
    public WineDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/WineDark.xaml") };
}

[Display(Name = "森林夜", GroupName = "纯色深色", Description = "森林绿自然深色主题", Order = 154, Prompt = "新增")]
public class ForestDarkColorResource : ResxColorResourceBase
{
    public ForestDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/ForestDark.xaml") };
}

[Display(Name = "终端绿", GroupName = "纯色深色", Description = "终端荧光绿深色主题", Order = 155, Prompt = "新增")]
public class TerminalGreenDarkColorResource : ResxColorResourceBase
{
    public TerminalGreenDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/TerminalGreenDark.xaml") };
}

[Display(Name = "珍珠白", GroupName = "纯色浅色", Description = "柔和中性珍珠白主题", Order = 170, Prompt = "新增")]
public class PearlLightColorResource : ResxColorResourceBase
{
    public PearlLightColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/PearlLight.xaml") };
}

[Display(Name = "天际蓝", GroupName = "纯色浅色", Description = "清爽天空蓝浅色主题", Order = 171, Prompt = "新增")]
public class SkyLightColorResource : ResxColorResourceBase
{
    public SkyLightColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/SkyLight.xaml") };
}

[Display(Name = "鼠尾草", GroupName = "纯色浅色", Description = "低饱和绿色浅色主题", Order = 172, Prompt = "新增")]
public class SageLightColorResource : ResxColorResourceBase
{
    public SageLightColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/SageLight.xaml") };
}

[Display(Name = "蜜桃粉", GroupName = "纯色浅色", Description = "温柔蜜桃色浅色主题", Order = 173, Prompt = "新增")]
public class PeachLightColorResource : ResxColorResourceBase
{
    public PeachLightColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/PeachLight.xaml") };
}

[Display(Name = "湖水青", GroupName = "纯色浅色", Description = "湖水青浅色主题", Order = 174, Prompt = "新增")]
public class AquaLightColorResource : ResxColorResourceBase
{
    public AquaLightColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/AquaLight.xaml") };
}

[Display(Name = "云雾灰", GroupName = "纯色浅色", Description = "灰蓝色办公浅色主题", Order = 175, Prompt = "新增")]
public class CloudLightColorResource : ResxColorResourceBase
{
    public CloudLightColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/CloudLight.xaml") };
}
