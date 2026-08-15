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

[Display(Name = "Entity Framework Core", GroupName = "数据与日志", Description = "用于 SQLite 和 SQL Server 的数据访问。")]
public class EntityFrameworkCoreDependencyItem : DependencyItemBase
{
    public EntityFrameworkCoreDependencyItem()
    {
        Name = "Entity Framework Core";
        Author = "Microsoft";
        Uri = "https://github.com/dotnet/efcore";
        Version = "8.0.16";
        Licence = "MIT License";
        Description = "用于 SQLite 和 SQL Server 的数据访问。";
    }
}
