// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using H.Services.Common;
using H.Mvvm;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Serialization;

namespace H.Modules.Project
{
    [Display(Name = "工程管理", GroupName = SettingGroupNames.GroupData, Description = "应用此功能可以管理工程信息")]
    public class ProjectViewPresenter : IProjectViewPresenter
    {
        private readonly IProjectService _projectService;
        public ProjectViewPresenter(IProjectService projectService)
        {
            this._projectService = projectService;
        }
        [Browsable(false)]
        [XmlIgnore]
        public RelayCommand NewCommand => new RelayCommand(async (s, e) =>
        {
            var project = this._projectService.Create();
            var r = await IocMessage.Form.ShowEdit(project, null, null, null, "新建工程");
            if (r != true)
                return;
            this._projectService.Add(project);
            this._projectService.Current = project;
        }, (s, e) => this._projectService != null);

        [Browsable(false)]
        [XmlIgnore]
        public RelayCommand NewOrListCommand => new RelayCommand((s, e) =>
        {
            if (this._projectService.Where().Count() == 0)
            {
                this.NewCommand.Execute(null);
                return;
            }
            this.ListCommand.Execute(null);

        }, (s, e) => this._projectService != null);

        [Browsable(false)]
        [XmlIgnore]
        public RelayCommand ListCommand => new RelayCommand(async (s, e) =>
        {
            var project = new ProjectListViewPresenter();
            project.SelectedItem = this._projectService.Current;
            var r = await IocMessage.Dialog.Show(project, x =>
            {
                x.Title = "选择工程";
                x.MinWidth = 600;
                x.MinHeight = 400;
            });
            if (project.SelectedItem == null)
                return;
            if (r == true)
                this._projectService.Current = project.SelectedItem;
        }, (s, e) => this._projectService != null && this._projectService.Where().Count() > 0);
    }
}
