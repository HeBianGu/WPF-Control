// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Dashboard;
//### **4. 复古胶片风（Vintage Film）**
//** 特点**：暖调褪色感，适合摄影、文艺类应用  
//```xml
//<!-- 背景 -->
//<Color x:Key="Vintage.Background">#F0E6D2</Color> <!-- 奶油纸 -->
//<Color x:Key="Vintage.Surface">#E5D5B8</Color> <!-- 旧书页 -->

//<!-- 主色 -->
//<Color x:Key="Vintage.Primary">#8B5A2B</Color>  <!-- 棕褐 -->
//<Color x:Key="Vintage.Secondary">#6D4C41</Color> <!-- 咖啡 -->
//<Color x:Key="Vintage.Accent">#9C786C</Color>  <!-- 玫瑰棕 -->

//<!-- 文字 -->
//<Color x:Key="Vintage.Text">#4A3A2E</Color>
//<Color x:Key="Vintage.TextLight">#8B7D6E</Color>
[Display(Name = "复古胶片", GroupName = "推荐主题", Description = "复古胶片推荐主题", Order = 101, Prompt = "推荐")]
public class VintageFilmColorResource : ResxColorResourceBase
{
    public VintageFilmColorResource()
    {
        this.IsDark = true;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Dashboard;component/Light.xaml")
    };
}
