// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Services.Message;
global using Microsoft.Extensions.DependencyInjection;
global using System.ComponentModel.DataAnnotations;
using H.Common.Interfaces;
using H.Services.Common.Setting;

namespace H.Services.Common.SplashScreen;

[Icon("\xE74E")]
[Display(Name = "保存所有配置", Description = "保存应用中所有需要保存的数据")]
public class IocSaveAllCommand : DisplayMarkupCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        IEnumerable<ISplashSave> saves = Ioc.Services.GetServices<ISplashSave>();
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

                {
                    if (f.IsCancel)
                        return false;
                    x.Value = "正在保存系统配置";
                    var r = Ioc.Services.GetService<ISettingDataService>().Save(out string message);
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
        return Ioc.Services != null && Ioc.Services.GetServices<ISplashSave>().Count() > 0;
    }
}