﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Services.Common;
using H.Mvvm.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace H.Styles.Default
{
    [Icon("\xE7E8")]
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
                if (SettingDataManager.Instance != null)
                {
                    var r = SettingDataManager.Instance.Save(out string message);
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
                        {
                            SaveSplash(d, s);
                        }
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
}
