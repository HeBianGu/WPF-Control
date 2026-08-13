// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")


// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.Attributes;
using H.Extensions.Mvvm.ViewModels.Base;

namespace H.Modules.Plugin.Base;

public class PluginData
{
    [Display(Name = "名称")]
    public string Name { get; set; }
    [Display(Name = "说明")]
    public string Description { get; set; }
    [Display(Name = "分组")]
    public string GroupName { get; set; }
    [Display(Name = "路径")]
    public string DllPath { get; set; }
    [Display(Name = "全名")]
    public string FullName { get; set; }
    [Display(Name = "时间")]
    public string DateTime { get; set; }
    [Display(Name = "大小")]
    public string FileSize { get; set; }
}
