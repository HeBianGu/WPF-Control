// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Controls.Form.Attributes;
using H.Controls.Form.PropertyItem.TextPropertyItems;
using H.Extensions.Setting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Setting;

[Display(Name = "设置页面控制", GroupName = SettingGroupNames.GroupSecurity)]
internal class SettingSecurityViewOption : IocOptionInstance<SettingSecurityViewOption>, ISettingSecurityViewOption
{
    private bool _usePassword;
    [DefaultValue(false)]
    [Display(Name = "启用密码", Description = "设置是否启用密码，启用后进入设置页面需要输入预设管理员密码")]
    public bool UsePassword
    {
        get { return _usePassword; }
        set
        {
            _usePassword = value;
        }
    }

    private bool _rememberPassword;
    [DefaultValue(true)]
    [Display(Name = "记住密码", Description = "记住密码后下次打开设置页面自动进入")]
    public bool RememberPassword
    {
        get { return _rememberPassword; }
        set
        {
            _rememberPassword = value;
        }
    }

    private string _password;
    [PropertyItem(typeof(PasswordTextPropertyItem))]
    [DefaultValue("123456")]
    [Display(Name = "设置管理员密码", Description = "设置是否启用密码，启用后进入设置页面需要输入预设管理员密码")]
    public string Password
    {
        get { return _password; }
        set
        {
            _password = value;
        }
    }

    private int _passwordCount;
    [DefaultValue(3)]
    [Display(Name = "密码校验次数", Description = "密码输入错误次数超过密码校验次数不许再输入")]
    public int PasswordCount
    {
        get { return _passwordCount; }
        set
        {
            _passwordCount = value;
        }
    }
}
