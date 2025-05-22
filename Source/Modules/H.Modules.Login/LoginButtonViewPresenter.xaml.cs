// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Extensions.Mvvm.ViewModels.Base;
using H.Services.Setting;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Login
{
    [Icon(FontIcons.Contact)]
    [Display(Name = "登录工具", GroupName = SettingGroupNames.GroupSystem, Description = "显示登录工具按钮")]
    public class LoginButtonViewPresenter : BindableBase, ILoginButtonViewPresenter
    {

    }

    public interface ILoginButtonViewPresenter
    {

    }
}
