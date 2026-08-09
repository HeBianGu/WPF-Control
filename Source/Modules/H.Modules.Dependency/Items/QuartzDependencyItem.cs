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

[Display(Name = "Quartz", GroupName = "任务与更新", Description = "用于计划任务调度。")]
public class QuartzDependencyItem : DependencyItemBase
{
    public QuartzDependencyItem()
    {
        Name = "Quartz";
        Uri = "https://github.com/quartznet/quartznet";
        Version = "3.14.0";
        Licence = "Apache License 2.0";
        Description = "用于计划任务调度。";
    }
}
