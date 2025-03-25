// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Message.Dialog.Commands;

public class ShowPercentCommand : ShowMessageDialogCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        Func<ICancelable, IPercentPresenter, bool> func = (c, p) =>
        {
            for (int i = 0; i <= 100; i++)
            {
                if (c.IsCancel)
                    break;
                p.Value = i;
                Thread.Sleep(50);
            }
            return true;
        };
        await IocMessage.Dialog.ShowPercent(func, x => this.Invoke(x));
    }
}
