using H.Common.Attributes;
using H.Common.Commands;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace H.Extensions.Log4net;

[Icon("\xEC87")]
[Display(Name = "查看日志", Description = "显示日志路径")]
public class ShowLog4netPathCommand : DisplayMarkupCommandBase
{
    public override Task ExecuteAsync(object parameter)
    {
        Process.Start(new ProcessStartInfo(Log4netOptions.Instance.LogPath) { UseShellExecute = true });
        return base.ExecuteAsync(parameter);
    }

    public override bool CanExecute(object parameter)
    {
        return base.CanExecute(parameter) && Log4netOptions.Instance != null;
    }

}
