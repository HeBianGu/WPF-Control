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

[Display(Name = "CommunityToolkit.Mvvm", GroupName = "基础框架", Description = "提供 MVVM 基础功能。")]
public class CommunityToolkitMvvmDependencyItem : DependencyItemBase
{
    public CommunityToolkitMvvmDependencyItem()
    {
        Name = "CommunityToolkit.Mvvm";
        Author = "Microsoft";
        Uri = "https://github.com/CommunityToolkit/dotnet";
        Version = "README 未注明";
        Licence = "MIT License";
        Description = "提供 MVVM 基础功能。";
    }
}
