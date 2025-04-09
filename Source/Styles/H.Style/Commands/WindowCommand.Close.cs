// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Styles.Controls;

namespace H.Styles.Commands;

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
