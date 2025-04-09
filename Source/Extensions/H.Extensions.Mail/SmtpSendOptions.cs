using H.Common.Attributes;
using H.Extensions.Setting;
using H.Services.Setting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Extensions.Mail;

public class SmtpSendOptions<T> : IocOptionInstance<T>, ISmtpSendOptions where T : class, new()
{
    private string _host;
    [Required]
    [DefaultValue("smtp.163.com")]
    [Display(Name = "服务器")]
    public string Host
    {
        get { return _host; }
        set
        {
            _host = value;
            RaisePropertyChanged();
        }
    }


    private int _port;
    [DefaultValue(25)]
    [Display(Name = "端口号")]
    public int Port
    {
        get { return _port; }
        set
        {
            _port = value;
            RaisePropertyChanged();
        }
    }


    private bool _enableSsl;
    [DefaultValue(true)]
    [Display(Name = "启用SSL")]
    public bool EnableSsl
    {
        get { return _enableSsl; }
        set
        {
            _enableSsl = value;
            RaisePropertyChanged();
        }
    }

    private string _user;
    [Required]
    [DefaultValue("HeBianGu2024@163.com")]
    [Display(Name = "外发账号")]
    public string User
    {
        get { return _user; }
        set
        {
            _user = value;
            RaisePropertyChanged();
        }
    }


    private string _password;
    [Required]
    [Display(Name = "授权码")]
    public string Password
    {
        get { return _password; }
        set
        {
            _password = value;
            RaisePropertyChanged();
        }
    }

    private bool _isBodyHtml;
    [Display(Name = "启用HTML格式")]
    public bool IsBodyHtml
    {
        get { return _isBodyHtml; }
        set
        {
            _isBodyHtml = value;
            RaisePropertyChanged();
        }
    }

}

[Icon("\xE89C")]
[Display(Name = "邮件外发设置", GroupName = SettingGroupNames.GroupSystem, Description = "邮件外发设置的信息")]
public class SmtpSendOptions : SmtpSendOptions<SmtpSendOptions>
{

}
