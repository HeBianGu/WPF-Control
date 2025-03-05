// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Common
{
    public class ShowDeleteProjectDialogCommand : IocAsyncMarkupCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter is IProjectItem project)
            {
                await IocProject.Instance.ShowDeleteProjectDialog(project);
            }
        }
        public override bool CanExecute(object parameter)
        {
            return IocProject.Instance != null;
        }
    }

}