// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Services.Message.Dialog.Commands;

namespace H.Modules.Identity.Commands
{
    [Icon(FontIcons.People)]
    [Display(Name = "角色管理", GroupName = SettingGroupNames.GroupAuthority, Description = "应用此功能进行角色管理")]
    public class ShowRoleViewCommand : ShowIocCommand
    {
        public ShowRoleViewCommand()
        {
            this.Type = typeof(IRoleViewPresenter);
        }
    }
}

