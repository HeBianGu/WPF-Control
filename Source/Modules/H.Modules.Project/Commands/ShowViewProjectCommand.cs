// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Modules.Project.Commands;

[Icon("\xE70F")]
[Display(Name = "查看项目", Description = "显示选中项目查看页面")]
public class ShowViewProjectCommand : ShowProjectCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        IProjectItem projectItem = IocProject.Instance.Current;
        if (parameter is IProjectItem project)
            projectItem = project;
        if (projectItem == null)
            return;
        await IocProject.Instance.ShowViewProject(projectItem, x => this.Invoke(x));
    }
}