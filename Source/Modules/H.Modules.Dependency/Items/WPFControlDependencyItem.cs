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

using H.Modules.Dependency.Base;

namespace H.Modules.Dependency.Items;

[Display(Name = "WPF-Control", GroupName = "开源控件库", Description = "License (Ms-PL)许可的旧版本")]
public class WPFControlDependencyItem : DependencyItemBase
{
    public WPFControlDependencyItem()
    {
        Name = "WPF-Control";
        Author = "HeBianGu";
        Uri = "https://github.com/HeBianGu/WPF-Control";
        Version = "1.3.0";
        Licence = "MIT License";
        Description = "基于 .NET 8+ 的高性能 WPF 控件库";
    }
}
