// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Common
{
    public static class ProjectExtension
    {
        public static async Task<bool?> ShowNewProjectDialog(this IProjectService projectService, string defalultName = "项目")
        {
            var project = projectService.Create();
            if (project == null)
                return false;
            if (string.IsNullOrEmpty(project.Title))
            {
                project.Title = defalultName + (projectService.Where().Count() + 1).ToString();
            }
            var r = await IocMessage.Form.ShowEdit(project, x => x.Title = "新建工程");
            if (r != true)
                return r;
            projectService.Add(project);
            projectService.Current = project;
            return true;
        }

        public static async Task<bool?> ShowProjectsDialog(this IProjectService projectService)
        {
            var project = projectService.Create();
            if (project == null)
                return false;
            if (string.IsNullOrEmpty(project.Title))
            {
                project.Title = "项目" + (projectService.Where().Count() + 1).ToString();
            }
            var r = await IocMessage.Form.ShowEdit(project, x => x.Title = "新建工程");
            if (r != true)
                return r;
            projectService.Add(project);
            projectService.Current = project;
            return true;
        }

        public static async Task<bool?> ShowDeleteProjectDialog(this IProjectService projectService, IProjectItem project)
        {
            var r = await IocMessage.Dialog.Show("确定要删除？");
            if (r != true)
                return r;
            projectService.Delete(x => x == project);
            return projectService.Save(out string message);
        }

        public static async Task<bool?> ShowOpenProjectDialog(this IProjectService projectService, IProjectItem project)
        {
            string message = null;
            projectService.Current = project;
            var r = await IocMessage.Dialog.ShowWait(x =>
           {
               x.Title = "正在打开工程...";
               return projectService.Save(out string message);
           });
            if (r == false && !string.IsNullOrEmpty(message))
                await IocMessage.ShowDialogMessage(message);
            return r;
        }
        public static async Task<bool?> ShowSaveProjectsDialog(this IProjectService projectService)
        {
            string message = null;
            var r = await IocMessage.Dialog.ShowWait(x =>
            {
                return projectService.Save(out message);

            }, x => x.Title = "正在保存工程列表...");
            if (r == false && !string.IsNullOrEmpty(message))
                await IocMessage.ShowDialogMessage(message);
            return r;
        }

        public static async Task<bool?> ShowSaveCurrentProjectDialog(this IProjectService projectService)
        {
            string message = null;
            var c = projectService.Current;
            if (c == null)
                return false;
            var r = await IocMessage.Dialog.ShowWait(x =>
            {
                Thread.Sleep(500);
                if (!projectService.Contain(c))
                    projectService.Add(c);
                return projectService.Current.Save(out message);

            }, x => x.Title = $"正在保存<{c.Title}>...");
            if (r == false && !string.IsNullOrEmpty(message))
                await IocMessage.ShowDialogMessage(message);
            return r;
        }

        public static bool Contain(this IProjectService projectService, IProjectItem projectItem)
        {
            return projectService.Where(x => x == projectItem).Count() > 0;
        }
    }
}