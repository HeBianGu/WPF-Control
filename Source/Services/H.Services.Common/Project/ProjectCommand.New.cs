// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Common
{
    public class ProjectNewCommand : IocAsyncMarkupCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            IProjectItem project = IocProject.Instance.Create();
            bool? r = await IocMessage.Form.ShowEdit(project, x => x.Title = "新建工程");
            if (r != true)
                return;
            IocProject.Instance.Add(project);
            IocProject.Instance.Current = project;
        }
        public override bool CanExecute(object parameter)
        {
            return IocProject.Instance != null && base.CanExecute(parameter);
        }
    }

}