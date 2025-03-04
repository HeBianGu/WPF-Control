// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using H.Mvvm;
using H.Services.Common;
using System.Threading.Tasks;

namespace H.Modules.Project
{
    public class ShowProjectsCommand : IocAsyncMarkupCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            var p = Ioc.GetService<IProjectViewPresenter>();
            await p.ShowProjectList();
        }
    }

    public class ShowNewProjectCommand : IocAsyncMarkupCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            var p = Ioc.GetService<IProjectViewPresenter>();
            await p.ShowProjectList();
        }
    }

    public class SaveProjectListCommand : IocAsyncMarkupCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            string message = null;
            var r = await IocMessage.Dialog.ShowWait(x =>
            {
                return IocProject.Instance?.Save(out message);

            }, x => x.Title = "正在保存工程列表...");
            if (r == false && !string.IsNullOrEmpty(message))
                await IocMessage.ShowDialogMessage(message);
        }
    }


    public class SaveCurrentProjectCommand : IocAsyncMarkupCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            string message = null;
            var current = IocProject.Instance.Current;
            if (current == null)
                return;
            var r = await IocMessage.Dialog.ShowWait(x =>
            {
                return IocProject.Instance.Current?.Save(out message);

            }, x => x.Title = $"正在保存工程<{current.Title}>...");
            if (r == false && !string.IsNullOrEmpty(message))
                await IocMessage.ShowDialogMessage(message);
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter) && IocProject.Instance.Current != null;
        }
    }
}
