// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Extensions.Setting;
using H.Modules.Help.Base;
using H.Services.Setting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Help.WebSite;
[Icon(FontIcons.Home)]
[Display(Name = "官方网址", GroupName = SettingGroupNames.GroupSystem, Description = "查看官方网址")]
public class WebsiteOptions : UriHelpOptionsBase<WebsiteOptions>, IWebsiteOptions
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.Uri = "https://hebiangu.github.io/WPF-Control/Home.html";
    }
}
