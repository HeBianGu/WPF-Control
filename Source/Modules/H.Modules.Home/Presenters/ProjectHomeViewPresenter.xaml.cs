// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Extensions.Mvvm.Commands;
using H.Extensions.Mvvm.ViewModels.Base;
using H.Globalization.Properties;
using H.Iocable;
using H.Modules.Home.Base;
using H.Mvvm.Commands;
using H.Services.Message;
using H.Services.Message.Dialog;
using H.Services.Project;
using Microsoft.Extensions.Options;
using System;
using System.Windows.Markup;

namespace H.Modules.Home.Presenters;

public class GetExampleProjectsExtension : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return IocProject.Instance?.Where(x => x.IsExample);
    }
}
public class ProjectHomeViewPresenter : CommandsBindableBase, IHomeViewPresenter
{
    private readonly IOptions<HomeOptions> _options;
    public ProjectHomeViewPresenter(IOptions<HomeOptions> options)
    {
        _options = options;
    }


    public RelayCommand ShowProjectCommand => new RelayCommand(x =>
    {
        Ioc.GetService<IProjectDialogService>()?.ShowProjectsDialog();
    }, x => Ioc.GetService<IProjectDialogService>() != null);


    [Icon("\xE77F")]
    [Display(Name = "打开项目", Description = "打开当前选中项目")]
    public DisplayCommand ShowOpenProjectCommand => new DisplayCommand(x =>
    {
        if (x is IProjectItem project)
        {
            Ioc.GetService<IProjectDialogService>()?.ShowOpenProject(project);
        }

    }, x => Ioc.GetService<IProjectDialogService>() != null);

    [Icon("\xE70F")]
    [Display(Name = "编辑项目", Description = "显示选中项目编辑页面")]
    public DisplayCommand ShowEidtProjectCommand => new DisplayCommand(async x =>
    {
        if (x is IProjectItem project)
        {
            var projectService = Ioc.GetService<IProjectService>();
            var r = await IocMessage.Form.ShowEdit(project, x =>
            {
                x.Title = "编辑项目";
            }, null, x =>
            {
                x.UseCommand = false;
            });
            if (r == true)
                projectService.Save(out string message);
        }
    }, x => Ioc.GetService<IProjectDialogService>() != null);

    [Icon(FontIcons.Delete)]
    [Display(Name = "删除项目", Description = "删除当前选中的项目")]
    public DisplayCommand ShowDeleteProjectCommand => new DisplayCommand(async x =>
    {
        if (x is IProjectItem project)
        {
            var projectService = Ioc.GetService<IProjectService>();
            var r = await IocMessage.Dialog.Show(Resources.Dialog_Message_ConfirmDelete);
            if (r != true)
                return;
            await projectService.DeleteAsync(x => x == project);
            projectService.Save(out string message);
        }
    }, x => Ioc.GetService<IProjectDialogService>() != null);

}
