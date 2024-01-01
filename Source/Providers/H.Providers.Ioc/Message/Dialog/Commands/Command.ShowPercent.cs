// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Threading;

namespace H.Providers.Ioc
{
    public class ShowPercentCommand : MessageCommandBase
    {
        public override void Execute(object parameter)
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
            IocMessage.Dialog.ShowPercent(func);
        }
    }
}
