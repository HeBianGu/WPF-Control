// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
namespace H.Modules.Project.Commands;

[Icon("\xE7C3")]
[Display(Name = "新建项目", Description = "显示新建项目页面")]
public class ShowNewProjectCommand : ShowProjectCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        await IocProject.Instance.ShowNewProject(x => this.Invoke(x));
    }
}