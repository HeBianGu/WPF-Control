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

[Display(Name = "纸张薄荷", GroupName = "混合配色", Description = "纸白、墨绿、薄荷、蓝灰混合，适合阅读、标注和长时间办公", Order = 150, Prompt = "混合")]
public class PaperMintColorResource : ResxColorResourceBase
{
    public PaperMintColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Office;component/PaperMint.xaml") };
}

[Display(Name = "暖色工作室", GroupName = "混合配色", Description = "奶油白、咖啡棕、珊瑚和琥珀混合，适合创作、轻办公和文档管理", Order = 151, Prompt = "混合")]
public class WarmStudioColorResource : ResxColorResourceBase
{
    public WarmStudioColorResource() => this.IsDark = false;
    public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Office;component/WarmStudio.xaml") };
}
