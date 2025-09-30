// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Services.Identity.Author;
using H.Services.Message.Dialog.Commands;

namespace H.Modules.Identity.Commands
{
    [Icon(FontIcons.Edit)]
    [Display(Name = "权限管理", GroupName = SettingGroupNames.GroupAuthority, Description = "应用此功能进行权限管理")]
    public class ShowAuthorityViewCommand : ShowIocCommand
    {
        public ShowAuthorityViewCommand()
        {
            this.Type = typeof(IAuthorityViewPresenter);
        }
    }
}

