using H.Common.Attributes;
using H.Common.Commands;
using H.Iocable;
using System.ComponentModel.DataAnnotations;

namespace H.Services.Logger;

/// <summary>
/// 记录日志命令，用于将系统日志记录到文件中。
/// </summary>
[Icon("\xE77F")]
[Display(Name = "记录日志", Description = "记录系统日志到文件中")]
public class LogCommand : DisplayMarkupCommandBase
{
    /// <summary>
    /// 获取或设置要记录的日志消息。
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 获取或设置日志的类型。
    /// </summary>
    public LogType Type { get; set; }

    /// <summary>
    /// 执行记录日志命令。
    /// </summary>
    /// <param name="parameter">命令参数。</param>
    public override void Execute(object parameter)
    {
        if (this.Type == LogType.Debug)
            Ioc<ILogService>.Instance?.Debug(this.Message);
        if (this.Type == LogType.Info)
            Ioc<ILogService>.Instance?.Info(this.Message);
        if (this.Type == LogType.Error)
            Ioc<ILogService>.Instance?.Error(this.Message);
        if (this.Type == LogType.Warn)
            Ioc<ILogService>.Instance?.Warn(this.Message);
        if (this.Type == LogType.Fatal)
            Ioc<ILogService>.Instance?.Fatal(this.Message);
    }
}
