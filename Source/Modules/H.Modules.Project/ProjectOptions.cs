using H.Extensions.Setting;
using H.Services.AppPath;
using H.Services.Setting;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace H.Modules.Project;

[Display(Name = "工程配置", GroupName = SettingGroupNames.GroupSystem, Description = "工程配置的信息")]
public class ProjectOptions : IocOptionInstance<ProjectOptions>, IProjectOptions
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.DefaultProjectFolder = AppPaths.Instance.Project;
    }
    private string _extenstion;
    [DefaultValue(".prj")]
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

    private string _defaultProjectFolder;
    [Display(Name = "文件存储路径")]
    public string DefaultProjectFolder
    {
        get { return _defaultProjectFolder; }
        set
        {
            _defaultProjectFolder = value;
            RaisePropertyChanged();
        }
    }

    private string _defaultProjectName;
    [DefaultValue("项目")]
    [Display(Name = "默认文件名称")]
    public string DefaultProjectName
    {
        get { return _defaultProjectName; }
        set
        {
            _defaultProjectName = value;
            RaisePropertyChanged();
        }
    }

    [XmlIgnore]
    [JsonIgnore]
    [Browsable(false)]
    public IJsonSerializerService JsonSerializerService { get; set; } = new TextJsonSerializerService();

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
