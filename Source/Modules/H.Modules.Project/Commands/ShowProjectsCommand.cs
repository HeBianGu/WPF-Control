// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
namespace H.Modules.Project.Commands;

[Icon("\xED25")]
[Display(Name = "打开项目", Description = "显示项目列表，选择要打开的项目")]
public class ShowProjectsCommand : ShowProjectCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        await IocProject.Instance.ShowProjectsDialog(x => this.Invoke(x));
    }
}
