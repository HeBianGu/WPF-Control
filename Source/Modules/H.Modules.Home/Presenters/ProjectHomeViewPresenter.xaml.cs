// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Iocable;
using H.Modules.Home.Base;
using H.Mvvm.Commands;
using H.Services.Project;
using Microsoft.Extensions.Options;

namespace H.Modules.Home.Presenters;
public class ProjectHomeViewPresenter : IHomeViewPresenter
{
    private readonly IOptions<HomeOptions> _options;
    public ProjectHomeViewPresenter(IOptions<HomeOptions> options)
    {
        _options = options;
    }

    public RelayCommand ShowProjectCommand => new RelayCommand(x =>
    {
        Ioc.GetService<IProjectDialogService>()?.ShowProjectsDialog();
    }, x => Ioc.GetService<IProjectDialogService>() != null);
}
