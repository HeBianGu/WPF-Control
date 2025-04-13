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
