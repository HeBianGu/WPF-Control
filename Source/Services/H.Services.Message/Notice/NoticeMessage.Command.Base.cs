// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
global using H.Common.Attributes;
global using System.ComponentModel.DataAnnotations;
namespace H.Services.Message.Notice;

[Icon("\xE77F")]
[Display(Name = "显示通知", Description = "显示通知消息")]
public abstract class ShowNoticeMessageCommandBase : DisplayMarkupCommandBase
{
    public string Message { get; set; } = "默认消息";

    public override void Execute(object parameter)
    {
        Ioc<INoticeMessageService>.Instance.ShowInfo(this.Message);
    }
}
