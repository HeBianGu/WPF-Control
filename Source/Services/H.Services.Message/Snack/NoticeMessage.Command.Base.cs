// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Message.Snack;

[Icon("\xE77F")]
[Display(Name = "显示提示", Description = "显示提示消息")]
public abstract class ShowSnackMessageCommandBase : DisplayMarkupCommandBase
{
    public string Message { get; set; } = "默认消息";

    public override void Execute(object parameter)
    {
        Ioc<ISnackMessageService>.Instance.ShowInfo(this.Message);
    }
}
