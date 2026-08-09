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

[Display(Name = "log4net", GroupName = "数据与日志", Description = "用于应用日志记录。")]
public class Log4NetDependencyItem : DependencyItemBase
{
    public Log4NetDependencyItem()
    {
        Name = "log4net";
        Uri = "https://logging.apache.org/log4net/";
        Version = "2.0.15";
        Licence = "Apache License 2.0";
        Description = "用于应用日志记录。";
    }
}
