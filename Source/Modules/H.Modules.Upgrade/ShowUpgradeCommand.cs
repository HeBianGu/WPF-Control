// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Commands;
using H.Services.Common.Upgrade;

namespace H.Modules.Upgrade;

[Icon("\xECC5")]
[Display(Name = "软件更新", Description = "应用此功能检查软件更新")]
public class ShowUpgradeCommand : DisplayMarkupCommandBase
{
    private IUpgradeService Service => Ioc<IUpgradeService>.Instance;
    public override bool CanExecute(object parameter)
    {
        return this.Service != null && base.CanExecute(parameter);
    }
    public override async Task ExecuteAsync(object parameter)
    {
        var r = await this.Service.ShowUpgradeAsync();
        if (r.message != null)
            await IocMessage.ShowDialogMessage(r.message);
    }
}
