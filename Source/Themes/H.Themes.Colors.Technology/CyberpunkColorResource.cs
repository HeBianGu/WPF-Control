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

//### **1. 深色科技风（Cyberpunk / Dark Tech）**
//** 特点**：霓虹对比色 + 深色背景，适合数据看板、科技感应用  
//```xml
//<!-- 背景 -->
//<Color x:Key="DarkTech.Background">#0D0F14</Color>
//<Color x:Key="DarkTech.Surface">#1A1D26</Color>

//<!-- 霓虹色 -->
//<Color x:Key="DarkTech.Primary">#00F0FF</Color>  <!-- 赛博蓝 -->
//<Color x:Key="DarkTech.Secondary">#FF2A6D</Color> <!-- 荧光粉 -->
//<Color x:Key="DarkTech.Accent">#05E0A7</Color>  <!-- 电子绿 -->

//<!-- 文字 -->
//<Color x:Key="DarkTech.Text">#E0E0E0</Color>
//<Color x:Key="DarkTech.TextDim">#8A8F9D</Color>
[Display(Name = "赛博朋克", GroupName = "科技未来", Description = "赛博朋克科技未来主题", Order = 413, Prompt = "新增")]
public class CyberpunkColorResource : ResxColorResourceBase
{
    public CyberpunkColorResource()
    {
        this.IsDark = true;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/CyberpunkDark.xaml")
    };
}
