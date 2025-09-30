// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Services.Setting;

namespace H.Modules.Project;

[Display(Name = "工程管理", GroupName = SettingGroupNames.GroupData, Description = "应用此功能可以管理工程信息")]
public class ProjectViewPresenter : IProjectViewPresenter
{
    private readonly IProjectService _projectService;
    public ProjectViewPresenter(IProjectService projectService)
    {
        this._projectService = projectService;
    }
}
