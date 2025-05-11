// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Services.Identity;
using H.Services.Message.Dialog.Commands;
using H.Services.Setting;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Modules.Login.Commands;

[Icon(FontIcons.SwitchUser)]
[Display(Name = "切换用户", GroupName = SettingGroupNames.GroupSystem, Description = "切换登录的账号")]
public class SwitchUserCommand : IocCommandBase<ILoginService>
{
    public override Task ExecuteAsync(object parameter)
    {
        if (Application.Current is ILoginableApplication loginable)
            loginable.Login();
        return base.ExecuteAsync(parameter);
    }
    public override bool CanExecute(object parameter)
    {
        return base.CanExecute(parameter) && this.Service?.User != null;
    }
}
