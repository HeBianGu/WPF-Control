// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Modules.Help.Base;
using H.Services.Setting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Help.ReleaseVersions;

[Icon(FontIcons.History)]
[Display(Name = "发行说明", GroupName = SettingGroupNames.GroupSystem, Description = "查看软件发行说明")]
public class ReleaseVersionsOptions : UriHelpOptionsBase<ReleaseVersionsOptions>, IReleaseVersionsOptions
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.Uri = "https://github.com/HeBianGu/WPF-Control/releases";
    }
}
