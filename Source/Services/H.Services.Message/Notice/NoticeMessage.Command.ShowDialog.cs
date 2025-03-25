// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Message.Notice;

public class ShowDialogNoticeMessageCommand : ShowNoticeMessageCommandBase
{
    public override async void Execute(object parameter)
    {
        bool? r = await Ioc<INoticeMessageService>.Instance.ShowDialog(this.Message);
        if (r == true)
            Ioc<INoticeMessageService>.Instance.ShowSuccess(this.Message);
        else
            Ioc<INoticeMessageService>.Instance.ShowError(this.Message);
    }
}
