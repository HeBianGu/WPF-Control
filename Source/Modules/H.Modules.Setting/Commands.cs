// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Services.Common;

using H.Mvvm;
using System.Windows;

namespace H.Modules.Setting
{
    public class ShowSettingCommand : MarkupCommandBase
    {
        public override async void Execute(object parameter)
        {
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
