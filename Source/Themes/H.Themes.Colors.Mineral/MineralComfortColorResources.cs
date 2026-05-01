// Copyright (c) HeBianGu Authors. All Rights Reserved.
// Author: HeBianGu
// Github: https://github.com/HeBianGu/WPF-Control
// Document: https://hebiangu.github.io/WPF-Control-Docs
// QQ:908293466 Group:971261058
// bilibili: https://space.bilibili.com/370266611
// Licensed under the MIT License (the "License")

using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Mineral;

[Display(Name = "森林雾绿", GroupName = "自然舒适", Description = "低饱和森林雾绿，适合长时间工作", Order = 100, Prompt = "低疲劳")]
public class ForestMistColorResource : ResxColorResourceBase
{
    public ForestMistColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Mineral;component/ForestMist.xaml") };
}

[Display(Name = "海风蓝", GroupName = "自然舒适", Description = "清爽海风蓝，适合低疲劳阅读和办公", Order = 101, Prompt = "低疲劳")]
public class OceanBreezeColorResource : ResxColorResourceBase
{
    public OceanBreezeColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Mineral;component/OceanBreeze.xaml") };
}

[Display(Name = "山岩灰", GroupName = "自然舒适", Description = "山岩灰蓝中性色，稳定克制", Order = 102, Prompt = "低疲劳")]
public class MountainSlateColorResource : ResxColorResourceBase
{
    public MountainSlateColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Mineral;component/MountainSlate.xaml") };
}

[Display(Name = "苔藓绿", GroupName = "自然舒适", Description = "柔和苔藓绿，贴近自然", Order = 103, Prompt = "低疲劳")]
public class MossGreenColorResource : ResxColorResourceBase
{
    public MossGreenColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Mineral;component/MossGreen.xaml") };
}

[Display(Name = "陶土暖棕", GroupName = "自然舒适", Description = "陶土暖棕与米色搭配，温暖柔和", Order = 104, Prompt = "低疲劳")]
public class ClayWarmColorResource : ResxColorResourceBase
{
    public ClayWarmColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Mineral;component/ClayWarm.xaml") };
}

[Display(Name = "沙漠暖黄", GroupName = "自然舒适", Description = "沙漠暖黄大地色，轻柔不刺眼", Order = 105, Prompt = "低疲劳")]
public class DesertSandColorResource : ResxColorResourceBase
{
    public DesertSandColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Mineral;component/DesertSand.xaml") };
}

[Display(Name = "湖面晨雾", GroupName = "自然舒适", Description = "湖面晨雾蓝绿灰，清晨感低饱和主题", Order = 106, Prompt = "低疲劳")]
public class LakeMorningColorResource : ResxColorResourceBase
{
    public LakeMorningColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Mineral;component/LakeMorning.xaml") };
}

[Display(Name = "雨天灰蓝", GroupName = "自然舒适", Description = "雨天灰蓝色，安静克制，适合专注", Order = 107, Prompt = "低疲劳")]
public class RainyDayColorResource : ResxColorResourceBase
{
    public RainyDayColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Mineral;component/RainyDay.xaml") };
}
