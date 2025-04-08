// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
global using System.Diagnostics;
using H.Services.Project;
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
    
        string p = System.IO.Path.Combine(current.Path?? ProjectOptions.Instance.DefaultProjectFolder, current.Title + ProjectOptions.Instance.Extenstion);
        Process.Start(new ProcessStartInfo("notepad", p) { UseShellExecute = true });
    }
}
