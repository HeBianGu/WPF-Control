using H.Common.Attributes;
using H.Extensions.Common;
using H.Extensions.FontIcon;
using H.Modules.Login.Base;
using H.Services.Identity;
using H.Services.Message.Dialog.Commands;
using H.Services.Setting;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows;

namespace H.Modules.Login.Commands;

[Icon(FontIcons.BlockContact)]
[Display(Name = "退出登录", GroupName = SettingGroupNames.GroupSystem, Description = "退出当前账号的登录")]
public class LogoutCommand : IocCommandBase<ILoginService>
{
    public override Task ExecuteAsync(object parameter)
    {
        this.Service.Logout(out string message);
        if (LoginOptions.Instance.UseLogoutRestart)
            Application.Current.Restart();
        return base.ExecuteAsync(parameter);
    }

    public override bool CanExecute(object parameter)
    {
        return base.CanExecute(parameter) && this.Service?.User != null;
    }
}
