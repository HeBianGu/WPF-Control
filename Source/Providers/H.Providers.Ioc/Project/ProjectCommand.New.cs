// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace H.Providers.Ioc
{
    public class ProjectNewCommand : IocMarkupCommandBase
    {
        public override async void Execute(object parameter)
        {
            var project = IocProject.Instance.Create();
            var r = await IocMessage.Form.ShowEdit(project, null, null, null, "新建工程");
            if (r != true)
                return;
            IocProject.Instance.Add(project);
            IocProject.Instance.Current = project;
        }
        public override bool CanExecute(object parameter)
        {
            return IocProject.Instance != null;
        }
    }

}