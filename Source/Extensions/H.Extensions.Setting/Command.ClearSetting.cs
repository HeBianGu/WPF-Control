// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Services.Common;
using H.Mvvm;
using System.Windows;
using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using H.Mvvm.Attributes;

namespace H.Extensions.Setting
{
    [Icon("\xE77F")]
    [Display(Name = "清理", Description = "清空所有配置数据，重启后数据恢复默认数据")]
    public class ClearSettingDataCommand : DisplayMarkupCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            var r = await IocMessage.ShowDialogMessage("清空配置数据无法恢复，确认清空配置？");
            if (r == false)
                return;

            r = AppPaths.Instance.ClearSetting();
            if (r == false)
                return;

            r = await IocMessage.ShowDialogMessage("重启后生效，是否立即关闭？");
            if (r == false)
                return;
            Application.Current.Shutdown();
        }
    }
}
