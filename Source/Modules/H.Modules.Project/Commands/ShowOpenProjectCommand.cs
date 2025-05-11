// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Modules.Project.Commands;

[Icon("\xE77F")]
[Display(Name = "打开项目", Description = "打开当前选中项目")]
public class ShowOpenProjectCommand : DisplayMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        if (parameter is IProjectItem project)
            await IocProject.Instance.ShowOpenProject(project);
    }
    public override bool CanExecute(object parameter)
    {
        return IocProject.Instance != null;
    }
}