// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Common.Attributes;

namespace H.Modules.Project.Commands;

[Icon("\xE70F")]
[Display(Name = "编辑项目", Description = "显示选中项目编辑页面")]
public class ShowEditProjectCommand : ShowProjectCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        IProjectItem projectItem = IocProject.Instance.Current;
        if (parameter is IProjectItem project)
            projectItem = project;
        if (projectItem == null)
            return;
        await IocProject.Instance.ShowEidtProject(projectItem, x => this.Invoke(x));
    }
}