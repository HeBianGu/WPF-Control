// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Services.Common;

using H.Mvvm;
using System.Windows;
using System.Threading.Tasks;
using H.Controls.Form;
using H.Presenters.Common;

namespace H.Modules.Setting
{
    public class ShowSettingCommand : AsyncMarkupCommandBase
    {
        private int _count = 0;
        private bool _successed;
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
                        IocMessage.Snack?.ShowInfo($"密码错误次数超过{SettingSecurityViewOption.Instance.PasswordCount}次禁止输入");
                        return false;
                    }
                    if (p.Password != SettingSecurityViewOption.Instance.Password)
                    {
                        _count++;
                        IocMessage.Snack?.ShowInfo($"密码错误[{_count}/{SettingSecurityViewOption.Instance.PasswordCount}]次");
                        return false;
                    }
                    return true;
                });
                if (pr != true)
                    return;
                _count = 0;
                if (SettingSecurityViewOption.Instance.RememberPassword)
                    this._successed = true;
            }
            SettingViewPresenter setting = new SettingViewPresenter();
            bool? r = await IocMessage.Dialog.Show(setting, x =>
            {
                x.Width = SettingViewOption.Instance.Width;
                x.Height = SettingViewOption.Instance.Height;
                x.Margin = SettingViewOption.Instance.Margin;
                x.MinWidth = SettingViewOption.Instance.MinWidth;
                x.MinHeight = SettingViewOption.Instance.MinHeight;
                x.HorizontalAlignment = HorizontalAlignment.Stretch;
                x.VerticalAlignment = VerticalAlignment.Stretch;
                x.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                x.VerticalContentAlignment = VerticalAlignment.Stretch;
                x.DialogButton = DialogButton.None;
                if (x is Window window)
                {
                    window.SizeToContent = SizeToContent.Manual;
                    window.ResizeMode = ResizeMode.CanResize;
                    window.ShowInTaskbar = true;
                    window.VerticalContentAlignment = VerticalAlignment.Stretch;
                }
            });
            if (r != true)
                return;
            bool sr = SettingDataManager.Instance.Save(out string error);
            if (sr == false)
            {
                await IocMessage.Dialog.Show(error);
            }
        }
    }

    public class SettingDefaultCommand : MarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            SettingDataManager.Instance.SetDefault();
        }
    }

    public class CancelSettingCommand : MarkupCommandBase
    {
        public override void Execute(object parameter)
        {
            SettingDataManager.Instance.Cancel();
        }
    }
}
