// Copyright (c) HeBianGu Authors. All Rights Reserved.
// Author: HeBianGu
// Github: https://github.com/HeBianGu/WPF-Control
// Document: https://hebiangu.github.io/WPF-Control-Docs
// QQ:908293466 Group:971261058
// bilibili: https://space.bilibili.com/370266611
// Licensed under the MIT License (the "License")

using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Office;

[Display(Name = "薄荷标注", GroupName = "标注办公", Description = "浅纸白、薄荷绿、蓝色、柔灰混合，适合长时间图像标注", Order = 170, Prompt = "混合")]
public class AnnotationMintColorResource : ResxColorResourceBase
{
    public AnnotationMintColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Office;component/AnnotationMint.xaml") };
}

[Display(Name = "标签工作室", GroupName = "标注办公", Description = "浅暖白、紫色、珊瑚、蓝色混合，适合标签管理和数据整理", Order = 171, Prompt = "混合")]
public class LabelStudioColorResource : ResxColorResourceBase
{
    public LabelStudioColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Office;component/LabelStudio.xaml") };
}

[Display(Name = "数据集板岩", GroupName = "标注办公", Description = "浅灰蓝、石板灰、蓝色、青色混合，适合数据集列表和样本筛选", Order = 172, Prompt = "混合")]
public class DatasetSlateColorResource : ResxColorResourceBase
{
    public DatasetSlateColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Office;component/DatasetSlate.xaml") };
}

[Display(Name = "图像实验室", GroupName = "标注办公", Description = "洁净白、医疗蓝、青绿、紫色混合，适合图像增强和图像分析", Order = 173, Prompt = "混合")]
public class ImageLabColorResource : ResxColorResourceBase
{
    public ImageLabColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Office;component/ImageLab.xaml") };
}

[Display(Name = "专注纸张", GroupName = "低疲劳办公", Description = "纸白、墨蓝、薄荷、琥珀混合，适合长时间阅读和配置", Order = 174, Prompt = "混合")]
public class FocusPaperColorResource : ResxColorResourceBase
{
    public FocusPaperColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Office;component/FocusPaper.xaml") };
}

[Display(Name = "静谧桌面", GroupName = "低疲劳办公", Description = "浅灰蓝、鼠尾草绿、蓝灰、沙色混合，适合后台、表单和属性面板", Order = 175, Prompt = "混合")]
public class CalmDeskColorResource : ResxColorResourceBase
{
    public CalmDeskColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Office;component/CalmDesk.xaml") };
}

[Display(Name = "暖色笔记", GroupName = "低疲劳办公", Description = "奶油白、咖啡棕、桃色、金色混合，适合轻办公和日志", Order = 176, Prompt = "混合")]
public class WarmNotebookColorResource : ResxColorResourceBase
{
    public WarmNotebookColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Office;component/WarmNotebook.xaml") };
}

[Display(Name = "柔和仪表盘", GroupName = "低疲劳办公", Description = "浅白、天蓝、薄荷、淡紫混合，适合浅色看板和统计界面", Order = 177, Prompt = "混合")]
public class SoftDashboardColorResource : ResxColorResourceBase
{
    public SoftDashboardColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Office;component/SoftDashboard.xaml") };
}
