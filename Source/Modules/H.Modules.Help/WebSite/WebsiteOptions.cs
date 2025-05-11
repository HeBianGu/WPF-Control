// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Modules.Help.Base;
using H.Services.Setting;
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
