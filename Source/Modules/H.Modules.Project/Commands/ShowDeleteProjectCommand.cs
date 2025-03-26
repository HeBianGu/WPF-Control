// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Modules.Project;
using H.Common.Commands;

namespace H.Modules.Project.Commands;

[Display(Name = "删除项目", Description = "删除当前选中的项目")]
public class ShowDeleteProjectCommand : DisplayMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        if (parameter is IProjectItem project)
            await IocProject.Instance.ShowDeleteProject(project);
    }
    public override bool CanExecute(object parameter)
    {
        return IocProject.Instance != null;
    }
}