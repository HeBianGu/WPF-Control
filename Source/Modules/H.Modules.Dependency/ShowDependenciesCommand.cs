// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Common.Attributes;
using H.Common.Commands;
using H.Extensions.FontIcon;
using H.Iocable;
using H.Services.Message.Dialog.Commands;

namespace H.Modules.Dependency;
[Icon(FontIcons.Contact)]
[Display(Name = "第三方依赖项", Description = "通过此方式查看项目中引用的第三方依赖项")]
public class ShowDependenciesCommand : ShowIocCommand
{
    public ShowDependenciesCommand()
    {
        this.Type = typeof(IDependencyViewPresenter);
    }
}

