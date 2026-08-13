// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Modules.Plugin.Base;
using H.Services.Message.Dialog.Commands;
using H.Services.Setting;

namespace H.Modules.Plugin;

[Icon(FontIcons.Component)]
[Display(Name = "插件管理", GroupName = SettingGroupNames.GroupAuthority, Description = "应用此功能查看插件管理")]
public class ShowPluginManagerCommand : ShowIocCommand
{
    public ShowPluginManagerCommand()
    {
        this.Type = typeof(IPluginManagerPresenter);
    }
}
