using H.Common.Attributes;
using H.Services.Common.About;
using H.Services.Message.Dialog.Commands;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace H.Modules.About;

[Icon("\xE946")]
[Display(Name = "服务协议", Description = "查看系统服务协议")]
public class ShowAgreementCommand : IocCommandBase<IAboutViewPresenter>
{
    public override Task ExecuteAsync(object parameter)
    {
        Process.Start(new ProcessStartInfo(AboutOptions.Instance.Agreement) { UseShellExecute = true });
        return Task.CompletedTask;
    }
}

