// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Message.Snack;

public class ShowSuccessSnackMessageCommand : ShowSnackMessageCommandBase
{
    public override void Execute(object parameter)
    {
        Ioc<ISnackMessageService>.Instance.ShowSuccess(this.Message);
    }
}
