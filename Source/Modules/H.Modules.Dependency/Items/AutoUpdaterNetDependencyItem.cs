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

[Display(Name = "AutoUpdater.NET", GroupName = "任务与更新", Description = "用于应用自动更新。")]
public class AutoUpdaterNetDependencyItem : DependencyItemBase
{
    public AutoUpdaterNetDependencyItem()
    {
        Name = "AutoUpdater.NET";
        Author = "ravibpatel";
        Uri = "https://github.com/ravibpatel/AutoUpdater.NET";
        Version = "README 未注明";
        Licence = "Apache License 2.0";
        Description = "用于应用自动更新。";
    }
}
