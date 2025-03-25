// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Message.Dialog.Commands;

public class ShowWaitCommand : ShowMessageDialogCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        Func<ICancelable, bool> func = c =>
        {
            Thread.Sleep(5000);
            return true;
        };
        await IocMessage.Dialog.ShowWait(func);
    }
}
