// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Message.Notice;

public class ShowInfoNoticeMessageCommand : ShowNoticeMessageCommandBase
{
    public override void Execute(object parameter)
    {
        IocMessage.Dialog.Show(this.Message);
        Ioc<INoticeMessageService>.Instance.ShowInfo(this.Message);
    }
}
