// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Services.Common.About;
using H.Services.Setting;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.About
{

    [Icon(FontIcons.Info)]
    [Display(Name = "关于", GroupName = SettingGroupNames.GroupSystem, Description = "这是一个关于页面的信息")]
    public class AboutViewPresenter : Ioc<AboutViewPresenter, IAboutViewPresenter>, IAboutViewPresenter
    {

    }
}
