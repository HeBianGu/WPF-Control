// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Services.Message;
global using H.Services.Message.Dialog;

namespace H.Windows.Main.Commands;

public class CloseWindowCommand : WindowCommandBase
{
    public bool UseDialog { get; set; } = false;
    public string Message { get; set; } = "确认退出系统?";
    public override async Task ExecuteAsync(object parameter)
    {
        if (parameter is Window window)
        {
            var r = await this.ShowDialogMessage(window);
            if (r != true)
                return;
            SystemCommands.CloseWindow(window);
        }
    }

    protected async Task<bool> ShowDialogMessage(Window window)
    {
        bool isMain = Application.Current.MainWindow == window && WindowSetting.Instance.UseNoticeOnMainWindowClose;
        if (isMain || this.UseDialog)
        {
            var r = await IocMessage.ShowDialogMessage(this.Message, "提示", DialogButton.SumitAndCancel);
            if (r != true)
                return false;
        }
        return true;
    }
}
