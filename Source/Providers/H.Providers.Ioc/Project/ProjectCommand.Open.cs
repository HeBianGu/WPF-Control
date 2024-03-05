// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Providers.Ioc
{
    public class ProjectOpenCommand : IocMarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            if (parameter is IProjectItem project)
            {
                IocProject.Instance.Current = project;
                IocProject.Instance.Save(out string message);
            }
        }
        public override bool CanExecute(object parameter)
        {
            return IocProject.Instance != null;
        }
    }

}