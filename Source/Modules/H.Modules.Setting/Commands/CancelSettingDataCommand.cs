// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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
