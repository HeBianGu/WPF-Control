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

[Display(Name = "Microsoft.Extensions.DependencyInjection", GroupName = "基础框架", Description = "用于 IOC 与依赖注入。")]
public class MicrosoftExtensionsDependencyInjectionDependencyItem : DependencyItemBase
{
    public MicrosoftExtensionsDependencyInjectionDependencyItem()
    {
        Name = "Microsoft.Extensions.DependencyInjection";
        Uri = "https://learn.microsoft.com/dotnet/core/extensions/dependency-injection";
        Version = "8.0.1";
        Licence = "MIT License";
        Description = "用于 IOC 与依赖注入。";
    }
}
