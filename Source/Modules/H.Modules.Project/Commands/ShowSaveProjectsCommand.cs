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
[Display(Name = "保存项目列表", Description = "保存项目列表到项目配置文件中")]
public class ShowSaveProjectsCommand : DisplayMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        await IocProject.Instance.ShowSaveProjects();
    }
    public override bool CanExecute(object parameter)
    {
        return IocProject.Instance != null;
    }
}