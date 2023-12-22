// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace H.Providers.Ioc
{
    public class ProjectSaveCommand : IocMarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            IocProject.Instance.Save(out string message);
        }
        public override bool CanExecute(object parameter)
        {
            return IocProject.Instance != null;
        }
    }

}