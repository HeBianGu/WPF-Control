// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


namespace H.Services.Common
{
    public class ShowSaveProjectsDialogCommand : IocAsyncMarkupCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            await IocProject.Instance.ShowSaveProjectsDialog();
        }
        public override bool CanExecute(object parameter)
        {
            return IocProject.Instance != null;
        }
    }

    public class ShowSaveCurrentProjectDialogCommand : IocAsyncMarkupCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            await IocProject.Instance?.ShowSaveCurrentProjectDialog();
        }
        public override bool CanExecute(object parameter)
        {
            return IocProject.Instance != null;
        }
    }
}