// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
global using H.Mvvm;
global using H.Services.Common;
global using System.Diagnostics;
namespace H.Modules.Project.Commands;

public class ShowCurrentProjectFileCommand : MarkupCommandBase
{
    public override void Execute(object parameter)
    {
        IProjectItem current = IocProject.Instance.Current;
        if (current == null)
            return;
        string p = System.IO.Path.Combine(current.Path, current.Title + ProjectOptions.Instance.Extenstion);
        Process.Start(new ProcessStartInfo("notepad", p) { UseShellExecute = true });

    }
}
