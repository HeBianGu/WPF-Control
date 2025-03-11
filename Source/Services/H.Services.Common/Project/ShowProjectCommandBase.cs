// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Common
{
    public abstract class ShowProjectCommandBase : ShowIocCommandBase<IProjectService>
    {
        public override bool CanExecute(object parameter)
        {
            return IocProject.Instance != null && base.CanExecute(parameter);
        }
    }
}