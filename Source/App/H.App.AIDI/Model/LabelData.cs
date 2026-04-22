// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Mvvm.ViewModels.Base;

namespace H.App.AIDI.Model;
public class LabelData : BindableBase
{
    [Display(Name = "类别")]
    public string Type { get; set; }

    [Display(Name = "图片")]
    public string Image { get; set; }

    [Display(Name = "类别属性")]
    public string TypeProperty { get; set; }

    [Display(Name = "集合")]
    public string List { get; set; }

    [Display(Name = "位置")]
    public string Location { get; set; }

    [Display(Name = "长边")]
    public int Length { get; set; }

    [Display(Name = "短边")]
    public int Width { get; set; }
}


