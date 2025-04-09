// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Extensions.Setting;
using H.Modules.Help.Base;
using H.Services.Setting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Help.Support;
[Icon(FontIcons.Group)]
[Display(Name = "技术支持", GroupName = SettingGroupNames.GroupSystem, Description = "查看软件技术支持")]
public class SupportOptions : UriHelpOptionsBase<SupportOptions>, ISupportOptions
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.Uri= "https://hebiangu.github.io/WPF-Control-Docs/";
    }
}
