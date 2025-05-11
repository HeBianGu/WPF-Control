// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Modules.Project.Commands;

[Icon("\xED25")]
[Display(Name = "打开项目", Description = "显示项目列表，选择要打开的项目")]
public class ShowProjectsCommand : ShowProjectCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        await IocProject.Instance.ShowProjectsDialog(x => this.Invoke(x));
    }
}
