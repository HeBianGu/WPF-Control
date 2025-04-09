using H.Common.Attributes;
using H.Services.Common.About;
using H.Services.Message.Dialog.Commands;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace H.Modules.About;

[Icon("\xE946")]
[Display(Name = "隐私政策", Description = "查看系统隐私政策")]
public class ShowPrivacyCommand : IocCommandBase<IAboutViewPresenter>
{
    public override Task ExecuteAsync(object parameter)
    {
        Process.Start(new ProcessStartInfo(AboutOptions.Instance.Privacy) { UseShellExecute = true });
        return Task.CompletedTask;
    }
}

