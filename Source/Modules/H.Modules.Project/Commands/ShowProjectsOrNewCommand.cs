// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Mvvm.Attributes;
using System.Threading.Tasks;

namespace H.Modules.Project.Commands;

[Icon("\xE77F")]
[Display(Name = "打开项目", Description = "打开项目列表页面，如果没有项目显示新增项目页面")]
public class ShowProjectsOrNewCommand : DisplayMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        await IocProject.Instance.ShowProjectsOrNewDialog();
    }
}
