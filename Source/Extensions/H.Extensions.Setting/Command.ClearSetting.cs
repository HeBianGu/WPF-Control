// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Services.Common;
using H.Mvvm;
using System.Windows;
using System;

namespace H.Extensions.Setting
{
    public class ClearSettingDataCommand : MarkupCommandBase
    {
        public override async void Execute(object parameter)
        {
            var r = await IocMessage.ShowDialogMessage("清空配置数据无法恢复，确认清空配置？");
            if (r == false)
                return;

            r = AppPaths.Instance.ClearSetting();
            if (r == false)
                return;

            r = await IocMessage.ShowDialogMessage("重启已生效，是否立即关闭？");
            if (r == false)
                return;
            Application.Current.Shutdown();
        }
    }
}
