// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Services.Common;
using System;
using System.Linq;
using System.Threading;
using System.Windows;

namespace H.Styles.Default
{
    public class CloseAfterSaveWindowCommand : CloseWindowCommand
    {
        public bool UseSave { get; set; } = true;
        public override async void Execute(object parameter)
        {
            if (Ioc.Services != null && this.UseSave)
            {
                var saves = Ioc.GetAssignableFromServices<ISplashSave>().ToList();
                if (saves.Count() > 0)
                {
                    var r = await IocMessage.Dialog.ShowString((f, x) =>
                    {
                        foreach (var save in saves)
                        {
                            if (f.IsCancel)
                                return false;
                            x.Value = "正在保存" + save.Name;
                            var r = save.Save(out string message);
                            if (r == false)
                            {
                                x.Value = message;
                                Thread.Sleep(1000);
                            }
                            Thread.Sleep(200);
                        }
                        return true;
                    }, x => x.DialogButton = DialogButton.Cancel);
                    if (r != true)
                        return;
                }
            }

            if (parameter is Window window)
            {
                bool isMain = Application.Current.MainWindow == window && WindowSetting.Instance.UseNoticeOnMainWindowClose;
                if (isMain || this.UseDialog)
                {
                    var r = await IocMessage.ShowDialogMessage(this.Message, "提示", DialogButton.SumitAndCancel);
                    if (r != true)
                        return;
                }
            }
            if (parameter is Window window1)
                SystemCommands.CloseWindow(window1);
        }
    }
}
