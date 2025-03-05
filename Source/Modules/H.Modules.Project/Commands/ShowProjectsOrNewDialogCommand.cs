// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using System.Threading.Tasks;

namespace H.Modules.Project.Commands;

public class ShowProjectsOrNewDialogCommand : IocAsyncMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        await IocProject.Instance.ShowProjectsOrNewDialog();
    }
}
