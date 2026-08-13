// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Setting;
using H.Services.AppPath;
using H.Services.Setting;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace H.Modules.Plugin;
[Display(Name = "插件设置", GroupName = SettingGroupNames.GroupStyle, Description = "插件设置的信息")]
public class PluginOptions : IocOptionInstance<PluginOptions>, IPluginOptions
{
    [ReadOnly(true)]
    [JsonIgnore]
    [XmlIgnore]
    [Display(Name = "插件路径")]
    public string PluginPath
    {
        get { return AppDomianPaths.Plugin; }
    }
}
