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

[Display(Name = "Newtonsoft.Json", GroupName = "数据与日志", Description = "用于项目数据的 JSON 序列化与反序列化。")]
public class NewtonsoftJsonDependencyItem : DependencyItemBase
{
    public NewtonsoftJsonDependencyItem()
    {
        Name = "Newtonsoft.Json";
        Author = "James Newton-King";
        Uri = "https://www.newtonsoft.com/json";
        Version = "13.0.3";
        Licence = "MIT License";
        Description = "用于项目数据的 JSON 序列化与反序列化。";
    }
}
