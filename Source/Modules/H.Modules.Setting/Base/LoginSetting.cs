// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Extensions.Setting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Setting.Base;

/// <summary> 登录 </summary>
[Display(Name = "登录", GroupName = SettingGroupNames.GroupBase)]
public class LoginSetting : Settable<LoginSetting>
{
    private bool _automaticStart;
    /// <summary> 开机自动启动  </summary>
    [DefaultValue(false)]
    [Display(Name = "开机自动启动")]
    public bool AutomaticStart
    {
        get { return _automaticStart; }
        set
        {
            _automaticStart = value;
            RaisePropertyChanged();
        }
    }

    private bool _automaticLogin;
    /// <summary> 开机自动启动  </summary>
    [DefaultValue(false)]
    [Display(Name = "启动时自动登录")]
    public bool AutomaticLogin
    {
        get { return _automaticLogin; }
        set
        {
            _automaticLogin = value;
            RaisePropertyChanged();
        }
    }

    private bool _automaticSavePasswod;
    /// <summary> 开机自动启动  </summary>
    [DefaultValue(false)]
    [Display(Name = "自动保存密码")]
    public bool AutomaticSavePasswod
    {
        get { return _automaticSavePasswod; }
        set
        {
            _automaticSavePasswod = value;
            RaisePropertyChanged();
        }
    }
}
