// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Attach;
using System.Reflection;

namespace H.Modules.Guide.Commands;
[Icon("\xE75A")]
[Display(Name = "新增功能", Description = "显示新增功能向导")]
public class ShowNewGuideCommand : ShowGuideCommandBase
{
    protected override bool IsMatch(UIElement element)
    {
        var version = Assembly.GetEntryAssembly().GetName().Version;
        if (version == null || version.ToString() == "1.0.0.0")
            return true;
        return Cattach.GetGuideAssemblyVersion(element) >= version;
    }
}
