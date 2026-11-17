// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Diagnostics;
using System.Security.Policy;

namespace H.Extensions.Command;

[Icon("\xE77F")]
[Display(Name = "运行进程", Description = "应用系统进程运行当前内容")]
public class ProcessCommand : DisplayMarkupCommandBase
{
    public string Uri { get; set; }
    public override bool CanExecute(object parameter)
    {
        var uri = this.GetUri(parameter);
        if (uri == null)
            return false;
        bool result = File.Exists(uri?.ToString()) || Directory.Exists(uri?.ToString());
        if (uri.ToString().ToLower().StartsWith("http") == true)
            return true;
        if (uri.ToString().ToLower().StartsWith("file:") == true)
            return true;
        return result;
    }

    protected virtual string GetUri(object parameter)
    {
        if (parameter == null)
            return this.Uri;
        return parameter.ToString();
    }

    public override void Execute(object parameter)
    {
        var uri = this.GetUri(parameter);
        if (uri == null)
            return;
        Process.Start(new ProcessStartInfo(uri?.ToString()) { UseShellExecute = true });
        base.Execute(parameter);
    }
}

[Icon("\xE77F")]
[Display(Name = "打开Uri", Description = "应用系统进程运行当前内容")]
public class MsedgeProcessCommand : DisplayMarkupCommandBase
{
    public string Uri { get; set; }
    public override bool CanExecute(object parameter)
    {
        var uri = this.GetUri(parameter);
        if (uri == null)
            return false;
        bool result = File.Exists(uri?.ToString()) || Directory.Exists(uri?.ToString());
        if (uri.ToString().ToLower().StartsWith("http") == true)
            return true;
        if (uri.ToString().ToLower().StartsWith("file:") == true)
            return true;
        return result;
    }

    protected virtual string GetUri(object parameter)
    {
        if (parameter == null)
            return this.Uri;
        return parameter.ToString();
    }

    public override void Execute(object parameter)
    {
        var uri = this.GetUri(parameter);
        if (uri == null)
            return;
        Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = "msedge.exe", // 或者 chrome.exe，或者默认浏览器的完整路径
            Arguments = uri,
            UseShellExecute = true // 这里设为 true 通常是为了能通过环境变量找到浏览器，或者直接写全路径
        });
        base.Execute(parameter);
    }
}
