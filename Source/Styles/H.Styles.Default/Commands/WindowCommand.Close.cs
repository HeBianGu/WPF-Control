// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Services.Common;
using System.Windows;

namespace H.Styles.Default
{
    public class CloseWindowCommand : WindowCommandBase
    {
        public bool UseDialog { get; set; } = false;
        public string Message { get; set; } = "确认退出系统?";
        public override async void Execute(object parameter)
        {
            if (parameter is Window window)
            {
                bool isMain = Application.Current.MainWindow == window && WindowSetting.Instance.UseNoticeOnMainWindowClose;
                if (isMain || this.UseDialog)
                {
                    var r = await IocMessage.ShowDialogMessage(this.Message, "提示", DialogButton.SumitAndCancel);
                    if (r != true)
                        return;
                }
                SystemCommands.CloseWindow(window);
            }
        }
    }
}
