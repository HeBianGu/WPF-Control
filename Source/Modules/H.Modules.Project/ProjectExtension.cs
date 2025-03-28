﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using System.Threading.Tasks;

namespace H.Modules.Project;

public static class ProjectExtension
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
