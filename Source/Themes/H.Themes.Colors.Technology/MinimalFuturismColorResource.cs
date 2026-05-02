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

//### **3. 极简未来（Minimal Futurism）**
//- ** 配色方案**：  
//  `#FFFFFF` (纯白) | `#F2F2F2` (浅灰) | `#2D2D2D` (炭黑) | `#00C4FF` (科技蓝) | `#FF3E41` (警示红)  
//- ** 特点**：干净简洁，突出功能感，适合App、智能设备界面。
[Display(Name = "极简未来", GroupName = "科技未来", Description = "极简未来科技未来主题", Order = 414, Prompt = "新增")]
public class MinimalFuturismColorResource : ResxColorResourceBase
{
    public MinimalFuturismColorResource()
    {
        this.IsDark = true;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Technology;component/MinimalFuturism.xaml")
    };
}
