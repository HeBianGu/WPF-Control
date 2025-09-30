// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Themes.Colors;
[Display(Name = "系统（长期支持）", GroupName = "纯色", Description = "纯色", Order = 10, Prompt = "长期支持")]
public class DefaultColorResource : ColorResourceBase
{
    public DefaultColorResource()
    {
        this.IsDark = false;
    }
    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Theme;component/ColorKeys.xaml")
    };
    public override string ToString()
    {
        return this.Name;
    }
}
