// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Mvvm.Attributes;
using System.ComponentModel.DataAnnotations;

namespace H.Services.Common
{
    [Icon("\xE77F")]
    [Display(Name = "保存项目列表", Description = "保存项目列表到项目配置文件中")]
    public class ShowSaveProjectsCommand : DisplayMarkupCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            await IocProject.Instance.ShowSaveProjects();
        }
        public override bool CanExecute(object parameter)
        {
            return IocProject.Instance != null;
        }
    }
}