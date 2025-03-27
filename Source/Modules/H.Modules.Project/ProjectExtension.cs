// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



global using H.Services.Message.Dialog;
global using H.Services.Project;

namespace H.Modules.Project;

public static partial class ProjectExtension
{
    public static async Task<bool?> ShowProjectsDialog(this IProjectService projectService, Action<IDialog> action = null)
    {
        ProjectListViewPresenter project = new ProjectListViewPresenter();
        project.SelectedItem = projectService.Current;
        bool? r = await IocMessage.ShowDialog(project, x =>
        {
            x.Title = "选择工程";
            x.MinWidth = 600;
            x.MinHeight = 400;
            x.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
            x.VerticalContentAlignment = System.Windows.VerticalAlignment.Stretch;
            x.Padding = new System.Windows.Thickness(0, 5, 0, 5);
            action?.Invoke(x);
        });
        if (project.SelectedItem == null)
            return false;
        if (r == true)
            return await projectService.ShowOpenProject(project.SelectedItem);
        return true;
    }

    public static async Task<bool?> ShowProjectsOrNewDialog(this IProjectService projectService)
    {
        return projectService.Current == null ? await projectService.ShowNewProject() : await projectService.ShowProjectsDialog();
    }
}

static partial class ProjectExtension
{
    public static async Task<bool?> ShowNewProject(this IProjectService projectService, Action<IDialog> action = null, string defalultName = "项目")
    {
        var project = projectService.Create();
        if (project == null)
            return false;
        if (string.IsNullOrEmpty(project.Title))
        {
            project.Title = defalultName + (projectService.Where().Count() + 1).ToString();
        }
        var r = await IocMessage.Form.ShowEdit(project, x =>
        {
            x.Title = "新建项目";
            action?.Invoke(x);
        }, null, x => x.UseCommand = false);
        if (r != true)
            return r;
        projectService.Add(project);
        projectService.Current = project;
        project.Save(out string message);
        return true;
    }
    public static async Task<bool?> ShowEidtProject(this IProjectService projectService, IProjectItem project, Action<IDialog> action = null)
    {
        return await IocMessage.Form.ShowEdit(project, x =>
        {
            x.Title = "编辑项目";
            action?.Invoke(x);
        }, null, x =>
        {
            x.UseCommand = false;
        });
    }

    //public static async Task<bool?> ShowProjects(this IProjectService projectService, Action<IDialog> action = null)
    //{
    //    var project = projectService.Create();
    //    if (project == null)
    //        return false;
    //    if (string.IsNullOrEmpty(project.Title))
    //    {
    //        project.Title = "项目" + (projectService.Where().Count() + 1).ToString();
    //    }
    //    var r = await IocMessage.Form.ShowEdit(project, action, null, x =>
    //    {
    //        x.UseCommand = false;
    //    });
    //    if (r != true)
    //        return r;
    //    projectService.Add(project);
    //    projectService.Current = project;
    //    return true;
    //}

    public static async Task<bool?> ShowDeleteProject(this IProjectService projectService, IProjectItem project)
    {
        var r = await IocMessage.Dialog.Show("确定要删除？");
        if (r != true)
            return r;
        projectService.Delete(x => x == project);
        return projectService.Save(out string message);
    }

    public static async Task<bool?> ShowOpenProject(this IProjectService projectService, IProjectItem project)
    {
        string message = null;
        projectService.Current = project;
        var r = await IocMessage.Dialog.ShowWait(x =>
        {
            x.Title = "正在打开工程...";
            project.UpdateTime = DateTime.Now;
            Thread.Sleep(2000);
            return projectService.Save(out string message);
        });
        if (r == false && !string.IsNullOrEmpty(message))
            await IocMessage.ShowDialogMessage(message);
        return r;
    }
    public static async Task<bool?> ShowSaveProjects(this IProjectService projectService)
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

    public static async Task<bool?> ShowSaveProject(this IProjectService projectService)
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
