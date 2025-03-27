using H.Extensions.Setting;
using H.Services.AppPath;
using H.Services.Setting;
using System.ComponentModel.DataAnnotations;

namespace H.Extensions.Log4net;

[Display(Name = "日志配置", GroupName = SettingGroupNames.GroupSystem, Description = "登录页面设置的信息")]
public class Log4netOptions : IocOptionInstance<Log4netOptions>
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.LogPath = IocAppPaths.Instance.Log;
        this.tempPath = IocAppPaths.Instance.Cache;
    }

    private string _logPath;
    [Display(Name = "日志路径")]
    public string LogPath
    {
        get { return _logPath; }
        set
        {
            _logPath = value;
            RaisePropertyChanged();
        }
    }

    private string _tempPath;
    [Display(Name = "缓存路径")]
    public string tempPath
    {
        get { return _tempPath; }
        set
        {
            _tempPath = value;
            RaisePropertyChanged();
        }
    }
}
