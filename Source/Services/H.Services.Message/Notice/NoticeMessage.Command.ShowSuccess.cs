// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Message.Notice;

public class ShowSuccessNoticeMessageCommand : ShowNoticeMessageCommandBase
{
    public override void Execute(object parameter)
    {
        Ioc<INoticeMessageService>.Instance.ShowSuccess(this.Message);
    }
}
