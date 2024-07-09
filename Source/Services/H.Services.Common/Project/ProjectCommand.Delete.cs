// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Common
{
    public class ProjectDeleteCommand : IocMarkupCommandBase
    {
        public override async void Execute(object parameter)
        {
            if (parameter is IProjectItem project)
            {
                bool? r = await IocMessage.Dialog.Show("确定要删除？");
                if (r != true) return;
                IocProject.Instance.Delete(x => x == project);
                IocProject.Instance.Save(out string message);
            }
        }
        public override bool CanExecute(object parameter)
        {
            return IocProject.Instance != null;
        }
    }

}