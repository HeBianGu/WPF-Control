// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Services.Common;

using H.Mvvm;
using System.Threading.Tasks;
using H.Mvvm.Attributes;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Setting
{
    [Icon("\xE713")]
    [Display(Name = "恢复默认", Description = "恢复系统设置默认数据")]
    public class SettingDefaultCommand : DisplayMarkupCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            var r = await IocMessage.ShowDialogMessage("清空配置数据无法恢复，确认清空配置？");
            if (r == false)
                return;
            IocSetting.Instance.SetDefault();
        }
    }
}
