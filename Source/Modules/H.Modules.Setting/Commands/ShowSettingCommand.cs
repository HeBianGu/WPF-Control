// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


global using H.Presenters.Common;
global using H.Services.Message.Dialog.Commands;
global using System.ComponentModel.DataAnnotations;
global using System.Windows;
using H.Extensions.FontIcon;

namespace H.Modules.Setting.Commands;

[Icon(FontIcons.Setting)]
[Display(Name = "设置", Description = "显示系统设置页面")]
public class ShowSettingCommand : ShowDialogCommandBase
{
    private int _count = 0;
    private bool _successed;
    public Type SwitchToType { get; set; }
    public override async Task ExecuteAsync(object parameter)
    {
        bool needInput = SettingSecurityViewOption.Instance.RememberPassword && this._successed;
        if (SettingSecurityViewOption.Instance.UsePassword && !needInput)
        {
            var p = new PasswordBoxPresenter();
            var pr = await IocDialog.Instance.Show(p, x =>
            {
                x.Width = 300;
                x.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                x.Title = "请输入密码";
            }, () =>
            {
                if (_count > SettingSecurityViewOption.Instance.PasswordCount)
                {
                    IocMessage.ShowSnackInfo($"密码错误次数超过{SettingSecurityViewOption.Instance.PasswordCount}次禁止输入");
                    return Task.FromResult(false);
                }
                if (p.Password != SettingSecurityViewOption.Instance.Password)
                {
                    _count++;
                    IocMessage.ShowSnackInfo($"密码错误[{_count}/{SettingSecurityViewOption.Instance.PasswordCount}]次");
                    return Task.FromResult(false);
                }
                return Task.FromResult(true);
            });
            if (pr != true)
                return;
            _count = 0;
            if (SettingSecurityViewOption.Instance.RememberPassword)
                this._successed = true;
        }
        await SettingViewPresenter.Instance.Show(this.SwitchToType ?? parameter?.GetType(), x => this.Invoke(x));
    }

    public override bool CanExecute(object parameter)
    {
        return base.CanExecute(parameter) && IocSetting.Instance != null && Ioc.GetService<ISettingViewPresenter>(false) != null;
    }
}
