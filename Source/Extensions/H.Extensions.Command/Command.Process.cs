// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System.Diagnostics;
using System.IO;

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
