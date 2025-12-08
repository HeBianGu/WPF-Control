// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Modules.Project.Commands;

[Icon("\xE74E")]
[Display(Name = "保存项目", Description = "保存当前选中向导到配置文件中")]
public class ShowSaveProjectCommand : ShowProjectCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        var current = IocProject.Instance?.Current;
        if (current == null)
            return;
        await IocProject.Instance?.ShowSaveProject(current);
    }
    public override bool CanExecute(object parameter)
    {
        return IocProject.Instance?.Current != null;
    }
}