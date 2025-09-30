// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Common.Commands;
using H.Attach;
using H.Services.Common.Guide;

namespace H.Modules.Guide.Commands;
[Icon("\xE75A")]
[Display(Name = "版本新增功能", Description = "显示版本新增功能向导")]
public class ShowVersionGuideCommand : DisplayMarkupCommandBase
{
    public Version Version { get; set; }
    public override async Task ExecuteAsync(object parameter)
    {
        var version = Version ?? Version.Parse(parameter?.ToString());
        if (version == null)
            await Ioc<IGuideService>.Instance.Show();
        else
        {

            await Ioc<IGuideService>.Instance.Show(x => Cattach.GetGuideAssemblyVersion(x) == version || version.ToString() == "1.0.0.0");
        }
    }
}
