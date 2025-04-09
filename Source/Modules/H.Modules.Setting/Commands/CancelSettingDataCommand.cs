// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
global using H.Services.Message;
global using H.Services.Message.Dialog;
global using H.Services.Setting;

namespace H.Modules.Setting.Commands;

public class CancelSettingDataCommand : DialogCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        var r = await IocMessage.ShowDialogMessage("取消配置将不会保存，是否继续？");
        if (r == false)
            return;
        IocSetting.Instance.Load(null, out string message);
        this.Cancel(parameter);
    }
}
