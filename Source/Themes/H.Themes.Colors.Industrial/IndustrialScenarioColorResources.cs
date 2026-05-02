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

[Display(Name = "钢铁灰蓝", GroupName = "工业制造", Description = "适合工控和设备监控的钢铁灰蓝主题", Order = 100, Prompt = "新增")]
public class FactorySteelColorResource : ResxColorResourceBase
{
    public FactorySteelColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Industrial;component/FactorySteel.xaml") };
}

[Display(Name = "安全橙黑", GroupName = "工业制造", Description = "适合警示与安全看板的橙黑主题", Order = 101, Prompt = "新增")]
public class SafetyOrangeColorResource : ResxColorResourceBase
{
    public SafetyOrangeColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Industrial;component/SafetyOrange.xaml") };
}

[Display(Name = "机床绿", GroupName = "工业制造", Description = "适合机床、产线和设备操作台的绿色主题", Order = 102, Prompt = "新增")]
public class MachineGreenColorResource : ResxColorResourceBase
{
    public MachineGreenColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Industrial;component/MachineGreen.xaml") };
}

[Display(Name = "警示琥珀", GroupName = "工业制造", Description = "适合设备状态、报警和能耗监控的琥珀主题", Order = 103, Prompt = "新增")]
public class WarningAmberColorResource : ResxColorResourceBase
{
    public WarningAmberColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Industrial;component/WarningAmber.xaml") };
}

[Display(Name = "车间深灰", GroupName = "工业制造", Description = "适合车间大屏和设备监控的深灰主题", Order = 104, Prompt = "新增")]
public class WorkshopDarkColorResource : ResxColorResourceBase
{
    public WorkshopDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Industrial;component/WorkshopDark.xaml") };
}

[Display(Name = "控制室蓝黑", GroupName = "工业制造", Description = "适合调度中心和控制室的蓝黑主题", Order = 105, Prompt = "新增")]
public class ControlRoomColorResource : ResxColorResourceBase
{
    public ControlRoomColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Industrial;component/ControlRoom.xaml") };
}

[Display(Name = "AI视觉深色", GroupName = "行业场景", Description = "适合 AI 视觉检测和图像分析的深色主题", Order = 120, Prompt = "新增")]
public class AIVisionDarkColorResource : ResxColorResourceBase
{
    public AIVisionDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Industrial;component/AIVisionDark.xaml") };
}

[Display(Name = "标注工具蓝", GroupName = "行业场景", Description = "适合数据标注和图像列表管理的蓝色主题", Order = 121, Prompt = "新增")]
public class AnnotationBlueColorResource : ResxColorResourceBase
{
    public AnnotationBlueColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Industrial;component/AnnotationBlue.xaml") };
}

[Display(Name = "训练控制台", GroupName = "行业场景", Description = "适合模型训练、日志输出和控制台监控的主题", Order = 122, Prompt = "新增")]
public class TrainingConsoleColorResource : ResxColorResourceBase
{
    public TrainingConsoleColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Industrial;component/TrainingConsole.xaml") };
}

[Display(Name = "数据集管理", GroupName = "行业场景", Description = "适合数据集、标签和文件管理的浅色主题", Order = 123, Prompt = "新增")]
public class DatasetManagerColorResource : ResxColorResourceBase
{
    public DatasetManagerColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Industrial;component/DatasetManager.xaml") };
}

[Display(Name = "安全运营", GroupName = "行业场景", Description = "适合安全运营和告警态势的深色主题", Order = 124, Prompt = "新增")]
public class SecurityOpsColorResource : ResxColorResourceBase
{
    public SecurityOpsColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Industrial;component/SecurityOps.xaml") };
}

[Display(Name = "医学影像", GroupName = "行业场景", Description = "适合医学影像阅片和医疗分析的浅色主题", Order = 125, Prompt = "新增")]
public class MedicalImageColorResource : ResxColorResourceBase
{
    public MedicalImageColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Industrial;component/MedicalImage.xaml") };
}

[Display(Name = "地图遥感深色", GroupName = "行业场景", Description = "适合地图、遥感和地理分析的深色主题", Order = 126, Prompt = "新增")]
public class GeoMapDarkColorResource : ResxColorResourceBase
{
    public GeoMapDarkColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Industrial;component/GeoMapDark.xaml") };
}

[Display(Name = "实验仪器", GroupName = "行业场景", Description = "适合实验室仪器和测量系统的浅色主题", Order = 127, Prompt = "新增")]
public class LabInstrumentColorResource : ResxColorResourceBase
{
    public LabInstrumentColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Industrial;component/LabInstrument.xaml") };
}

[Display(Name = "能源电网", GroupName = "行业场景", Description = "适合能源、电力和电网监控的深色主题", Order = 128, Prompt = "新增")]
public class EnergyGridColorResource : ResxColorResourceBase
{
    public EnergyGridColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Industrial;component/EnergyGrid.xaml") };
}

[Display(Name = "交通调度", GroupName = "行业场景", Description = "适合交通调度、信号监控和路线分析的深色主题", Order = 129, Prompt = "新增")]
public class TrafficControlColorResource : ResxColorResourceBase
{
    public TrafficControlColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Industrial;component/TrafficControl.xaml") };
}
