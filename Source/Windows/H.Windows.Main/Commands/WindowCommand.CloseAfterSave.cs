// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Common.Interfaces;
global using System.ComponentModel.DataAnnotations;
global using System.Windows;
using H.Iocable;
using H.Services.Setting;

namespace H.Windows.Main.Commands;
[Icon(FontIcons.ChromeClose)]
[Display(Name = "关闭并保存", Description = "关闭应用程序并保存应用数据")]
public class CloseAfterSaveWindowCommand : CloseWindowCommand
{
    public bool UseSave { get; set; } = true;
    public override async Task ExecuteAsync(object parameter)
    {
        Window window = parameter as Window;
        if (window == null)
            return;
        bool isMainWindow = window == Application.Current.MainWindow;
        string SaveSetting()
        {
            if (this.UseSave == false)
                return null;
            if (IocSetting.Instance != null)
            {
                var r = IocSetting.Instance.Save(out string message);
                if (r == false)
                    return message;
            }
            return null;
        }

        string SaveSplash(IDialog d, IStringPresenter s)
        {
            if (this.UseSave == false)
                return null;
            if (Ioc.Services == null)
                return null;
            var saves = Ioc.GetAssignableFromServices<ISplashSave>().ToList();
            if (saves.Count() > 0)
            {
                foreach (var save in saves)
                {
                    if (d?.IsCancel == true)
                        return null;
                    if (s != null)
                        s.Value = "正在保存" + save.Name;
                    var r = save.Save(out string message);
                    if (r == false && s != null)
                    {
                        s.Value = message;
                        Thread.Sleep(1000);
                    }
                    Thread.Sleep(200);
                }
            }
            return null;
        }

        if (WindowSetting.Instance.UseSaveOnMainWindowClose && isMainWindow)
        {
            if (IocMessage.Dialog != null)
            {
                var r = await IocMessage.Dialog?.ShowString((d, s) =>
                {
                    if (Ioc.Services != null && this.UseSave)
                        SaveSplash(d, s);
                    if (d.IsCancel)
                        return false;
                    s.Value = "正在保存系统设置";
                    var m = SaveSetting();
                    if (m != null)
                    {
                        s.Value = m;
                        Thread.Sleep(1000);
                    }
                    return true;

                }, x => x.DialogButton = DialogButton.Cancel);
                if (r != true)
                    return;
            }
            else
            {
                SaveSetting();
                SaveSplash(null, null);
            }
        }

        bool isNotice = isMainWindow && WindowSetting.Instance.UseNoticeOnMainWindowClose;
        if (isNotice || this.UseDialog)
        {
            var dr = await IocMessage.ShowDialogMessage(this.Message, "提示", DialogButton.SumitAndCancel);
            if (dr != true)
                return;
        }
        if (parameter is Window window1)
            SystemCommands.CloseWindow(window1);
    }
}
