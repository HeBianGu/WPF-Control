// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Modules.Project;

public class ProjectDialogService : IProjectDialogService
{
    public async Task<bool?> ShowOpenProject(IProjectItem project)
    {
        return await IocProject.Instance.ShowOpenProject(project);
    }

    public async Task<bool?> ShowProjectsDialog()
    {
        return await IocProject.Instance.ShowProjectsDialog(x => x.UseAddProject = true);
    }
}
