// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using H.Common.Attributes;
using H.Common.Commands;
using H.Services.Common.Upgrade;
using H.Services.Message;
using System.ComponentModel.DataAnnotations;

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
        if (this.Service.Upgrade(out string message) == false)
            await IocMessage.ShowDialogMessage(message);
    }
}
