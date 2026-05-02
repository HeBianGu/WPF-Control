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

/// <summary>
/// 芭比曙光粉：明亮而充满活力，如同晨曦中的第一缕阳光，温暖而希望。
/// </summary>
[Display(Name = "科技粉", GroupName = "科技未来", Description = "科技粉科技未来主题", Order = 418, Prompt = "新增")]
public class TechnologyPinkColorResource : ResxColorResourceBase
{
    public TechnologyPinkColorResource()
    {
        this.IsDark = false;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/TechnologyPink.xaml")
    };
}
