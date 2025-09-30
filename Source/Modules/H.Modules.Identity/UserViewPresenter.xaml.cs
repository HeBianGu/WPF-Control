// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Common.Interfaces.Where;
using H.Common.Attributes;
using H.Extensions.FontIcon;

namespace H.Modules.Identity
{
    public class Filter : IFilterable
    {
        public bool IsMatch(object obj)
        {
            return true;
        }
    }

    [Icon(FontIcons.AddFriend)]
    [Display(Name = "用户管理", GroupName = SettingGroupNames.GroupAuthority, Description = "应用此功能进行用户管理")]
    public class UserViewPresenter : IUserViewPresenter
    {

    }
}

