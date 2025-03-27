// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System.ComponentModel.DataAnnotations;

namespace H.Modules.Setting.Commands;

[Icon("\xE77F")]
[Display(Name = "取消", Description = "取消保存并系统设置页面")]
public class CancelSettingCommand : DisplayMarkupCommandBase
{
    public override void Execute(object parameter)
    {
        IocSetting.Instance.Cancel();
    }
}
