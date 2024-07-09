using H.Extensions.Setting;
using H.Services.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Login
{
    [Display(Name = "工程配置", GroupName = SettingGroupNames.GroupSystem, Description = "工程配置的信息")]
    public class ProjectOptions : IocOptionInstance<ProjectOptions>
    {
        private string _extenstion;
        [DefaultValue(".wf")]
        [Display(Name = "扩展名")]
        public string Extenstion
        {
            get { return _extenstion; }
            set
            {
                _extenstion = value;
                RaisePropertyChanged();
            }
        }

        private ProjectSaveMode _saveMode;
        [DefaultValue(ProjectSaveMode.OnProjectChanged)]
        [Display(Name = "保存的时机")]
        public ProjectSaveMode SaveMode
        {
            get { return _saveMode; }
            set
            {
                _saveMode = value;
                RaisePropertyChanged();
            }
        }

        //private string _historyPath;
        //[ReadOnly(true)]
        //[Display(Name = "历史数据保存位置")]
        //public string HistoryPath
        //{
        //    get { return _historyPath; }
        //    set
        //    {
        //        _historyPath = value;
        //        RaisePropertyChanged();
        //    }
        //}


        //public override void LoadDefault()
        //{
        //    base.LoadDefault();
        //    this.HistoryPath = System.IO.Path.Combine(AppPaths.Instance.UserProject, "Histroy.json");
        //}
    }
}
