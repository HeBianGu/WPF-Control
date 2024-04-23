// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading;

namespace H.Providers.Ioc
{
    public class IocSaveAllCommand : IocMarkupCommandBase
    {
        public override async void Execute(object parameter)
        {
            System.Collections.Generic.IEnumerable<ISplashSave> saves = System.Ioc.Services.GetServices<ISplashSave>();
            if (saves.Count() > 0)
            {
                bool r = await IocMessage.Dialog.ShowString((f, x) =>
                {
                    foreach (ISplashSave save in saves)
                    {
                        if (f.IsCancel)
                            return false;
                        x.Value = "正在保存" + save.Name;
                        bool r = save.Save(out string message);
                        if (r == false)
                        {
                            x.Value = message;
                            Thread.Sleep(1000);
                            return false;
                        }
                    }
                    return true;
                }, x => x.DialogButton = DialogButton.Cancel);
                if (r != true)
                    return;
            }
        }

        public override bool CanExecute(object parameter)
        {
            return System.Ioc.Services != null && System.Ioc.Services.GetServices<ISplashSave>().Count() > 0;
        }
    }
}