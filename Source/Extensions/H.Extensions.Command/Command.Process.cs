// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Diagnostics;

namespace H.Extensions.Command;

[Icon("\xE77F")]
[Display(Name = "运行进程", Description = "应用系统进程运行当前内容")]
public class ProcessCommand : DisplayMarkupCommandBase
{
    public override bool CanExecute(object parameter)
    {
        if (parameter == null) return false;
        bool result = File.Exists(parameter?.ToString()) || Directory.Exists(parameter?.ToString());
        if (parameter.ToString().ToLower().StartsWith("http") == true) return true;
        return result;
    }

    public override void Execute(object parameter)
    {
        //System.Diagnostics.Process.Start(parameter?.ToString());
        Process.Start(new ProcessStartInfo(parameter?.ToString()) { UseShellExecute = true });

    }
}
