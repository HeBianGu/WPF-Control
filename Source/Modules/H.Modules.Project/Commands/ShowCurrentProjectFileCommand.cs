// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using System.Diagnostics;
namespace H.Modules.Project.Commands;

[Icon("\xE8E5")]
[Display(Name = "打开项目文件", Description = "用记事本打开当前项目的配置文件数据")]
public class ShowCurrentProjectFileCommand : ShowProjectCommandBase
{
    public override void Execute(object parameter)
    {
        IProjectItem current = IocProject.Instance.Current;
        if (current == null)
            return;

        string p = System.IO.Path.Combine(current.Path ?? ProjectOptions.Instance.DefaultProjectFolder, current.Title + ProjectOptions.Instance.Extenstion);
        Process.Start(new ProcessStartInfo("notepad", p) { UseShellExecute = true });
    }
}
