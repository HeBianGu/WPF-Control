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

[Display(Name = "CSCore", GroupName = "媒体与音频", Description = "提供音频处理功能。")]
public class CSCoreDependencyItem : DependencyItemBase
{
    public CSCoreDependencyItem()
    {
        Name = "CSCore";
        Author = "filoe";
        Uri = "https://github.com/filoe/cscore";
        Version = "README 未注明";
        Licence = "MS-PL";
        Description = "提供音频处理功能。";
    }
}
