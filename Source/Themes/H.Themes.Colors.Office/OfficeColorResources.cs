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

[Display(Name = "行政蓝", GroupName = "专业办公", Description = "蓝灰稳重，适合后台、管理系统、生产平台", Order = 200, Prompt = "新增")]
public class ExecutiveBlueColorResource : ResxColorResourceBase
{
    public ExecutiveBlueColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Office;component/ExecutiveBlue.xaml") };
}

[Display(Name = "企业灰", GroupName = "专业办公", Description = "中性低干扰，适合长时间办公", Order = 201, Prompt = "新增")]
public class EnterpriseGrayColorResource : ResxColorResourceBase
{
    public EnterpriseGrayColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Office;component/EnterpriseGray.xaml") };
}

[Display(Name = "金融海军蓝", GroupName = "专业办公", Description = "深海军蓝，适合金融、报表和审批系统", Order = 202, Prompt = "新增")]
public class FinanceNavyColorResource : ResxColorResourceBase
{
    public FinanceNavyColorResource() => this.IsDark = true;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Office;component/FinanceNavy.xaml") };
}

[Display(Name = "文档墨蓝", GroupName = "专业办公", Description = "墨蓝文档风，适合资料管理和文书系统", Order = 203, Prompt = "新增")]
public class LegalInkColorResource : ResxColorResourceBase
{
    public LegalInkColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Office;component/LegalInk.xaml") };
}

[Display(Name = "医疗洁净白蓝", GroupName = "专业办公", Description = "洁净白蓝，适合医疗、影像和健康管理", Order = 204, Prompt = "新增")]
public class MedicalCleanColorResource : ResxColorResourceBase
{
    public MedicalCleanColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Office;component/MedicalClean.xaml") };
}

[Display(Name = "科研论文浅色", GroupName = "专业办公", Description = "论文纸张浅色，适合科研、实验和阅读", Order = 205, Prompt = "新增")]
public class ResearchPaperColorResource : ResxColorResourceBase
{
    public ResearchPaperColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Office;component/ResearchPaper.xaml") };
}

[Display(Name = "咖啡奶油", GroupName = "暖色人文", Description = "咖啡与奶油色，适合创作、阅读和轻办公", Order = 240, Prompt = "新增")]
public class CoffeeCreamColorResource : ResxColorResourceBase
{
    public CoffeeCreamColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Office;component/CoffeeCream.xaml") };
}

[Display(Name = "复古纸张", GroupName = "暖色人文", Description = "复古纸张色，适合阅读和文档编辑", Order = 241, Prompt = "新增")]
public class VintagePaperColorResource : ResxColorResourceBase
{
    public VintagePaperColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Office;component/VintagePaper.xaml") };
}

[Display(Name = "日落珊瑚", GroupName = "暖色人文", Description = "日落珊瑚色，适合创作和轻量办公", Order = 242, Prompt = "新增")]
public class SunsetCoralColorResource : ResxColorResourceBase
{
    public SunsetCoralColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Office;component/SunsetCoral.xaml") };
}

[Display(Name = "秋枫橙红", GroupName = "暖色人文", Description = "秋枫橙红色，适合阅读和创意工作", Order = 243, Prompt = "新增")]
public class AutumnMapleColorResource : ResxColorResourceBase
{
    public AutumnMapleColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Office;component/AutumnMaple.xaml") };
}

[Display(Name = "暖琥珀", GroupName = "暖色人文", Description = "暖琥珀色，适合轻办公和数据阅读", Order = 244, Prompt = "新增")]
public class WarmAmberColorResource : ResxColorResourceBase
{
    public WarmAmberColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Office;component/WarmAmber.xaml") };
}

[Display(Name = "书店暖棕", GroupName = "暖色人文", Description = "书店暖棕色，适合知识库、阅读和文档管理", Order = 245, Prompt = "新增")]
public class BookstoreColorResource : ResxColorResourceBase
{
    public BookstoreColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Office;component/Bookstore.xaml") };
}

[Display(Name = "肉桂棕", GroupName = "暖色人文", Description = "肉桂棕色，温暖柔和，适合轻办公", Order = 246, Prompt = "新增")]
public class CinnamonColorResource : ResxColorResourceBase
{
    public CinnamonColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Office;component/Cinnamon.xaml") };
}

[Display(Name = "老电影黄褐", GroupName = "暖色人文", Description = "老电影黄褐色，适合复古、阅读和文艺办公", Order = 247, Prompt = "新增")]
public class OldFilmColorResource : ResxColorResourceBase
{
    public OldFilmColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Office;component/OldFilm.xaml") };
}
