// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using H.Modules.Login;
using H.Mvvm;
using H.Services.Common;
using System.Diagnostics;
using System.Linq;
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
            await p.NewProject();
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
            if (IocProject.Instance.Where(x => x == current).Count() == 0)
                IocProject.Instance.Add(current);
            if (r == false && !string.IsNullOrEmpty(message))
                await IocMessage.ShowDialogMessage(message);
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter) && IocProject.Instance.Current != null;
        }
    }

    public class ShowCurrentProjectFileCommand : MarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            var current = IocProject.Instance.Current;
            if (current == null)
                return;

            var p =  System.IO.Path.Combine(current.Path, current.Title + ProjectOptions.Instance.Extenstion);
            Process.Start(new ProcessStartInfo("notepad",p) { UseShellExecute = true });

        }
    }
}
