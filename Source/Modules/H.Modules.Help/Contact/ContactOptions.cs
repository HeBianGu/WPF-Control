// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Common.Attributes;
using H.Controls.Form.Attributes;
using H.Controls.Form.PropertyItem.TextPropertyItems;
using H.Extensions.FontIcon;
using H.Extensions.Setting;
using H.Modules.Help.Base;
using H.Services.Setting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace H.Modules.Help.Contact;

[Icon(FontIcons.Contact)]
[Display(Name = "联系方式", GroupName = SettingGroupNames.GroupSystem, Description = "通过此方式可以联系到开发者")]
public class ContactOptions : IocOptionInstance<ContactOptions>, IContactOptions
{
    public override void LoadDefault()
    {
        base.LoadDefault();
    }

    private string _gitHub;
    [JsonIgnore]
    [XmlIgnore]
    [ReadOnly(true)]
    [PropertyItem(typeof(HyperlinkPropertyItem))]
    [DefaultValue("https://github.com/HeBianGu/WPF-Control/")]
    [Display(Name = "GitHub地址")]
    public string GitHub
    {
        get { return _gitHub; }
        set
        {
            _gitHub = value;
            RaisePropertyChanged();
        }
    }

    private string _gitHubIssue;
    [JsonIgnore]
    [XmlIgnore]
    [ReadOnly(true)]
    [PropertyItem(typeof(HyperlinkPropertyItem))]
    [DefaultValue("https://github.com/HeBianGu/WPF-Control/issues")]
    [Display(Name = "提出问题")]
    public string GitHubIssue
    {
        get { return _gitHubIssue; }
        set
        {
            _gitHubIssue = value;
            RaisePropertyChanged();
        }
    }

    private string _email;
    [JsonIgnore]
    [XmlIgnore]
    [ReadOnly(true)]
    [PropertyItem(typeof(HyperlinkPropertyItem))]
    [DefaultValue($"mailto:navylee0210@163.com?subject=subject&body=body")]
    [Display(Name = "发送邮箱")]
    public string Email
    {
        get { return _email; }
        set
        {
            _email = value;
            RaisePropertyChanged();
        }
    }

    private string _qq;
    [JsonIgnore]
    [XmlIgnore]
    [ReadOnly(true)]
    [PropertyItem(typeof(HyperlinkPropertyItem))]
    [DefaultValue("tencent://AddContact/?fromId=45&fromSubId=1&subcmd=all&uin={908293466}")]
    [Display(Name = "添加QQ")]
    public string QQ
    {
        get { return _qq; }
        set
        {
            _qq = value;
            RaisePropertyChanged();
        }
    }

    private string _podcast;
    [JsonIgnore]
    [XmlIgnore]
    [ReadOnly(true)]
    [PropertyItem(typeof(HyperlinkPropertyItem))]
    [DefaultValue("https://space.bilibili.com/370266611")]
    [Display(Name = "播客地址")]
    public string Podcast
    {
        get { return _podcast; }
        set
        {
            _podcast = value;
            RaisePropertyChanged();
        }
    }

    private string _blog;
    [JsonIgnore]
    [XmlIgnore]
    [ReadOnly(true)]
    [PropertyItem(typeof(HyperlinkPropertyItem))]
    [DefaultValue("https://blog.csdn.net/u010975589?type=blog")]
    [Display(Name = "博客地址")]
    public string Blog
    {
        get { return _blog; }
        set
        {
            _blog = value;
            RaisePropertyChanged();
        }
    }

}
