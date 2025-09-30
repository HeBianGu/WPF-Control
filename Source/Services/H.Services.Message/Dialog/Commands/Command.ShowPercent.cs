// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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
