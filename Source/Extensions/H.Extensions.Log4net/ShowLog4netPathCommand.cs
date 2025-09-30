// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Common.Commands;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace H.Extensions.Log4net;

[Icon("\xEC87")]
[Display(Name = "查看日志", Description = "显示日志路径")]
public class ShowLog4netPathCommand : DisplayMarkupCommandBase
{
    public override Task ExecuteAsync(object parameter)
    {
        Process.Start(new ProcessStartInfo(Log4netOptions.Instance.LogPath) { UseShellExecute = true });
        return base.ExecuteAsync(parameter);
    }

    public override bool CanExecute(object parameter)
    {
        return base.CanExecute(parameter) && Log4netOptions.Instance != null;
    }

}
