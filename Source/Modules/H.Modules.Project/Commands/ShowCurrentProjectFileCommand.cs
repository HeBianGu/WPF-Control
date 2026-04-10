// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using System.Diagnostics;
using H.Extensions.FontIcon;
namespace H.Modules.Project.Commands;

[Icon(FontIcons.View)]
[Display(Name = "打开项目文件", Description = "用记事本打开当前项目的配置文件数据")]
public class ShowCurrentProjectFileCommand : ShowProjectCommandBase
{
    public override void Execute(object parameter)
    {
        var current = IocProject.Instance?.Current;
        if (current == null)
            return;
        IocProject.Instance?.ShowCurrentProjectFile(current);
    }
    public override bool CanExecute(object parameter)
    {
        return IocProject.Instance?.Current != null;
    }
}
