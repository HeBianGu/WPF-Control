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
[Display(Name = "未来主义绿", GroupName = "科技未来", Description = "未来主义绿科技未来主题", Order = 410, Prompt = "新增")]
public class FuturisticGreenDarkColorResource : ResxColorResourceBase
{
    public FuturisticGreenDarkColorResource()
    {
        this.IsDark = true;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/FuturisticGreenDark.xaml")
    };
}
