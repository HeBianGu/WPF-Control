// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Modules.Project;
using H.Extensions.FontIcon;

namespace H.Modules.Project.Commands;

[Icon(FontIcons.Delete)]
[Display(Name = "删除项目", Description = "删除当前选中的项目")]
public class ShowDeleteProjectCommand : DisplayMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        if (parameter is IProjectItem project)
            await IocProject.Instance.ShowDeleteProject(project);
    }
    public override bool CanExecute(object parameter)
    {
        if (IocProject.Instance == null)
            return false;
        return IocProject.Instance.Current != parameter;
    }
}