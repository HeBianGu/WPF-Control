// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Iocable;
using H.Common.Commands;
using H.Services.Common.Guide;

namespace H.Modules.Guide.Commands;

public abstract class ShowGuideCommandBase : DisplayMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        await Ioc<IGuideService>.Instance.Show(this.IsMatch);
    }

    protected abstract bool IsMatch(UIElement element);
}
