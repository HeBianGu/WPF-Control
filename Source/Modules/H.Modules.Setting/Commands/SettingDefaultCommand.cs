// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


global using H.Common.Attributes;
global using H.Common.Commands;
global using System.ComponentModel.DataAnnotations;
using H.Services.AppPath;

namespace H.Modules.Setting.Commands;

[Icon("\xE713")]
[Display(Name = "恢复默认", Description = "恢复系统设置默认数据")]
public class SettingDefaultCommand : DisplayMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        var r = await IocMessage.ShowDialogMessage("恢复默认数据会清空配置数据无法恢复，确认恢复默认配置？");
        if (r == false)
            return;

        //r = AppPaths.Instance.ClearSetting(out string message);
        //if (r == false)
        //{
        //    await IocMessage.ShowDialogMessage(message);
        //    return;
        //}

        if (Application.Current is IConfigureableApplication configureable)
        {
            var groups = SettingViewPresenter.Instance.SelectedGroup;
            if (groups == null)
                return;
            var selectSettiable = groups.SelectedSettable ?? groups.Collection.FirstOrDefault();
            IocSetting.Instance.SetDefault();
            IocSetting.Instance.Clear();
            configureable.Configure();
            IocSetting.Instance.Load(null, out string message);
            SettingViewPresenter.Instance.RefreshSettingData();
            if (selectSettiable != null)
                SettingViewPresenter.Instance.SwitchTo(selectSettiable.GetType());
        }
    }
}
