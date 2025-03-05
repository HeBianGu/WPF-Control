// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Diagnostics;

namespace H.Services.Common
{
    public class ShowNewProjectDialogCommand : IocAsyncMarkupCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            await IocProject.Instance.ShowNewProjectDialog();
        }
    }
}