// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Providers.Ioc
{
    public class ShowWaitCommand : MessageCommandBase
    {
        public override void Execute(object parameter)
        {
            Func<ICancelable, bool> func = c =>
            {
                Thread.Sleep(5000);
                return true;
            };
            IocMessage.Dialog.ShowWait(func);
        }
    }
}
