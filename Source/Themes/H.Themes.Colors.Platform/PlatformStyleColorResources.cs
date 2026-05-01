// Copyright (c) HeBianGu Authors. All Rights Reserved.
// Author: HeBianGu
// Github: https://github.com/HeBianGu/WPF-Control
// Document: https://hebiangu.github.io/WPF-Control-Docs
// QQ:908293466 Group:971261058
// bilibili: https://space.bilibili.com/370266611
// Licensed under the MIT License (the "License")

using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Platform;

[Display(Name = "Modern Fluent", GroupName = "平台风格", Description = "现代 Fluent 氛围浅色主题", Order = 200, Prompt = "新增")]
public class ModernFluentColorResource : ResxColorResourceBase
{
    public ModernFluentColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Platform;component/ModernFluent.xaml") };
}

[Display(Name = "Material Soft", GroupName = "平台风格", Description = "柔和 Material 氛围浅色主题", Order = 201, Prompt = "新增")]
public class MaterialSoftColorResource : ResxColorResourceBase
{
    public MaterialSoftColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Platform;component/MaterialSoft.xaml") };
}

[Display(Name = "Cupertino Light", GroupName = "平台风格", Description = "清透 Cupertino 氛围浅色主题", Order = 202, Prompt = "新增")]
public class CupertinoLightColorResource : ResxColorResourceBase
{
    public CupertinoLightColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Platform;component/CupertinoLight.xaml") };
}

[Display(Name = "Git Dark", GroupName = "平台风格", Description = "代码托管平台氛围深色主题", Order = 203, Prompt = "新增")]
public class GitDarkColorResource : ResxColorResourceBase
{
    public GitDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Platform;component/GitDark.xaml") };
}

[Display(Name = "Terminal Classic", GroupName = "平台风格", Description = "经典终端黑绿主题", Order = 204, Prompt = "新增")]
public class TerminalClassicColorResource : ResxColorResourceBase
{
    public TerminalClassicColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Platform;component/TerminalClassic.xaml") };
}

[Display(Name = "Dashboard Blue", GroupName = "平台风格", Description = "仪表盘蓝色浅色主题", Order = 205, Prompt = "新增")]
public class DashboardBlueColorResource : ResxColorResourceBase
{
    public DashboardBlueColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Platform;component/DashboardBlue.xaml") };
}

[Display(Name = "Cloud Console", GroupName = "平台风格", Description = "云控制台蓝灰深色主题", Order = 206, Prompt = "新增")]
public class CloudConsoleColorResource : ResxColorResourceBase
{
    public CloudConsoleColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Platform;component/CloudConsole.xaml") };
}

[Display(Name = "IDE Blue Dark", GroupName = "平台风格", Description = "IDE 蓝黑深色主题", Order = 207, Prompt = "新增")]
public class IDEBlueDarkColorResource : ResxColorResourceBase
{
    public IDEBlueDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Platform;component/IDEBlueDark.xaml") };
}

[Display(Name = "Editor Dark Plus", GroupName = "平台风格", Description = "编辑器深色增强主题", Order = 208, Prompt = "新增")]
public class EditorDarkPlusColorResource : ResxColorResourceBase
{
    public EditorDarkPlusColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Platform;component/EditorDarkPlus.xaml") };
}

[Display(Name = "Solarized Light", GroupName = "平台风格", Description = "低对比 Solarized 浅色主题", Order = 209, Prompt = "新增")]
public class SolarizedLightColorResource : ResxColorResourceBase
{
    public SolarizedLightColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Platform;component/SolarizedLight.xaml") };
}

[Display(Name = "Solarized Dark", GroupName = "平台风格", Description = "低对比 Solarized 深色主题", Order = 210, Prompt = "新增")]
public class SolarizedDarkColorResource : ResxColorResourceBase
{
    public SolarizedDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Platform;component/SolarizedDark.xaml") };
}
