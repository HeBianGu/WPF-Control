// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Common.Attributes;
global using H.Extensions.FontIcon;
global using H.Mvvm.ViewModels.Base;
global using H.Services.Setting;
global using System.ComponentModel.DataAnnotations;

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
