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

[Display(Name = "QRCoder", GroupName = "开源控件库", Description = "用于二维码生成。")]
public class QRCoderDependencyItem : DependencyItemBase
{
    public QRCoderDependencyItem()
    {
        Name = "QRCoder";
        Uri = "https://github.com/codebude/QRCoder/";
        Version = "1.4.3";
        Licence = "MIT License";
        Description = "用于二维码生成。";
    }
}
