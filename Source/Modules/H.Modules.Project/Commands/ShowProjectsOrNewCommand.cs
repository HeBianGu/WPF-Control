// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Common.Commands;

namespace H.Modules.Project.Commands;

[Icon("\xED25")]
[Display(Name = "打开项目", Description = "打开项目列表页面，如果没有项目显示新增项目页面")]
public class ShowProjectsOrNewCommand : DisplayMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        await IocProject.Instance.ShowProjectsOrNewDialog();
    }
}
