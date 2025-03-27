// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Message.Dialog;

public class CancelDialogCommand : DialogCommandBase
{
    public override void Execute(object parameter)
    {
        this.Cancel(parameter);
    }
}
