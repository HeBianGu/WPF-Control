// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Message.Dialog.Commands;

public class ShowNotImplementedCommand : ShowMessageCommand
{
    public ShowNotImplementedCommand()
    {
        this.Message = "功能暂未实现";
    }
}
