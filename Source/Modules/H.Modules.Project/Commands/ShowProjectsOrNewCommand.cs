// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
global using H.Common.Commands;
using H.Services.Project;

namespace H.Modules.Project.Commands;

[Icon("\xED25")]
[Display(Name = "打开项目", Description = "打开项目列表页面，如果没有项目显示新增项目页面")]
public class ShowProjectsOrNewCommand : DisplayMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        await IocProject.Instance.ShowProjectsOrNewDialog();
    }
}
