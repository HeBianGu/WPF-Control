// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Common.Commands;
using H.Services.Common.Guide;

namespace H.Modules.Guide.Commands;

[Icon("\xE963")]
[Display(Name = "新手向导", Description = "显示新手向导")]
public class ShowGuideCommand : DisplayMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        await Ioc<IGuideService>.Instance.Show();
    }

    public override bool CanExecute(object parameter)
    {
        return base.CanExecute(parameter) && Ioc<IGuideService>.Instance != null;
    }
}
