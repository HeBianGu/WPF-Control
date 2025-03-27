// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Modules.Project.Commands;

[Icon("\xE74E")]
[Display(Name = "保存项目", Description = "保存当前选中向导到配置文件中")]
public class ShowSaveProjectCommand : ShowProjectCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        await IocProject.Instance?.ShowSaveProject();
    }
}