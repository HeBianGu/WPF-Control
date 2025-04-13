﻿using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Services.Identity;
using H.Services.Message.Dialog.Commands;
using H.Services.Setting;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Modules.Login.Commands;

[Icon(FontIcons.AddFriend)]
[Display(Name = "登录", GroupName = SettingGroupNames.GroupSystem, Description = "显示登录页面")]
public class LoginCommand : IocCommandBase<ILoginService>
{
    public override Task ExecuteAsync(object parameter)
    {
        if (Application.Current is ILoginableApplication loginable)
            loginable.Login();
        return base.ExecuteAsync(parameter);
    }
    public override bool CanExecute(object parameter)
    {
        return base.CanExecute(parameter) && this.Service?.User == null;
    }
}
