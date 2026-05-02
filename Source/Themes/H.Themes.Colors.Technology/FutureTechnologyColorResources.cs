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

[Display(Name = "赛博紫", GroupName = "科技未来", Description = "适合 AI、算法和监控大屏的赛博紫深色主题", Order = 300, Prompt = "科技未来")]
public class CyberVioletColorResource : ResxColorResourceBase
{
    public CyberVioletColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/CyberViolet.xaml") };
}

[Display(Name = "神经网络蓝", GroupName = "科技未来", Description = "适合 AI 神经网络、算法训练和数据可视化的蓝色主题", Order = 301, Prompt = "科技未来")]
public class NeuralBlueColorResource : ResxColorResourceBase
{
    public NeuralBlueColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/NeuralBlue.xaml") };
}

[Display(Name = "量子青", GroupName = "科技未来", Description = "适合量子计算、算法控制台和科技大屏的青色主题", Order = 302, Prompt = "科技未来")]
public class QuantumTealColorResource : ResxColorResourceBase
{
    public QuantumTealColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/QuantumTeal.xaml") };
}

[Display(Name = "数据流青蓝", GroupName = "科技未来", Description = "适合数据流、实时监控和可视化链路的青蓝主题", Order = 303, Prompt = "科技未来")]
public class DataStreamColorResource : ResxColorResourceBase
{
    public DataStreamColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/DataStream.xaml") };
}

[Display(Name = "全息蓝紫", GroupName = "科技未来", Description = "适合全息视觉、未来界面和科技展示的蓝紫主题", Order = 304, Prompt = "科技未来")]
public class HolographicColorResource : ResxColorResourceBase
{
    public HolographicColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/Holographic.xaml") };
}

[Display(Name = "AI 控制台黑蓝", GroupName = "科技未来", Description = "适合 AI 控制台、训练日志和运行监控的黑蓝主题", Order = 305, Prompt = "科技未来")]
public class AIConsoleColorResource : ResxColorResourceBase
{
    public AIConsoleColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/AIConsole.xaml") };
}

[Display(Name = "深空蓝紫", GroupName = "科技未来", Description = "适合深空大屏、科研可视化和未来系统的蓝紫主题", Order = 306, Prompt = "科技未来")]
public class DeepSpaceColorResource : ResxColorResourceBase
{
    public DeepSpaceColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/DeepSpace.xaml") };
}

[Display(Name = "卫星控制台", GroupName = "科技未来", Description = "适合卫星控制、遥测监控和态势大屏的深色主题", Order = 307, Prompt = "科技未来")]
public class SatelliteOpsColorResource : ResxColorResourceBase
{
    public SatelliteOpsColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/SatelliteOps.xaml") };
}
