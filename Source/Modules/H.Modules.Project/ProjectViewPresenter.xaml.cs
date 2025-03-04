// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using H.Services.Common;
using H.Mvvm;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Serialization;
using System.Threading.Tasks;
using H.Modules.Login;

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
        [System.Text.Json.Serialization.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public RelayCommand NewCommand => new RelayCommand(async e =>
        {
            await this.NewProject();
        }, e => this._projectService != null);


        public async Task<bool?> NewProject()
        {
            var project = this._projectService.Create();
            if (project == null)
                return false;
            if(string.IsNullOrEmpty(project.Title))
            {
                project.Title = ProjectOptions.Instance.DefaultProjectName + (this._projectService.Where().Count() + 1).ToString();
            }
            var r = await IocMessage.Form.ShowEdit(project, x => x.Title = "新建工程");
            if (r != true)
                return false;
            this._projectService.Add(project);
            this._projectService.Current = project;
            return true;
        }

        [Browsable(false)]
        [System.Text.Json.Serialization.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public RelayCommand NewOrListCommand => new RelayCommand(e =>
        {
            if (this._projectService.Where().Count() == 0)
            {
                this.NewCommand.Execute(null);
                return;
            }
            this.ListCommand.Execute(null);

        }, e => this._projectService != null);

        [Browsable(false)]
        [System.Text.Json.Serialization.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public RelayCommand ListCommand => new RelayCommand(async e =>
        {
            await this.ShowProjectList();
        }, e => this._projectService != null && this._projectService.Where().Count() > 0);

        public async Task<bool?> ShowProjectList()
        {
            var project = new ProjectListViewPresenter();
            project.SelectedItem = this._projectService.Current;
            var r = await IocMessage.Dialog.Show(project, x =>
            {
                x.Title = "选择工程";
                x.MinWidth = 600;
                x.MinHeight = 400;
                x.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
                x.VerticalContentAlignment = System.Windows.VerticalAlignment.Stretch;
                x.Padding = new System.Windows.Thickness(2);
            });
            if (project.SelectedItem == null)
                return false;
            if (r == true)
                this._projectService.Current = project.SelectedItem;
            return true;
        }
    }
}
