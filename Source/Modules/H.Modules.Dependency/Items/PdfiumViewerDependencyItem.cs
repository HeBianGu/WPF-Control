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

[Display(Name = "PdfiumViewer", GroupName = "开源控件库", Description = "用于 PDF 文档查看。")]
public class PdfiumViewerDependencyItem : DependencyItemBase
{
    public PdfiumViewerDependencyItem()
    {
        Name = "PdfiumViewer";
        Uri = "https://github.com/bezzad/PdfiumViewer";
        Version = "1.0.6";
        Licence = "Apache License 2.0";
        Description = "用于 PDF 文档查看。";
    }
}
