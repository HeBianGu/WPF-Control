// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Services.Message.Notice;

public interface INoticeMessageService
{
    Task<bool?> ShowDialog(string message);
    void ShowError(string message = "运行错误");
    void ShowFatal(string message = "严重错误");
    void ShowInfo(string message = "运行完成");
    void Show(INoticeItem message);
    Task<T> ShowProgress<T>(Func<IPercentNoticeItem, T> action);
    Task<T> ShowString<T>(Func<INoticeItem, T> action);
    void ShowSuccess(string message = "运行成功");
    void ShowWarn(string message = "异常警告");
}

public static class NoticeMessageServiceExtension
{
    public static void ShowErrorDispatcher(this INoticeMessageService service, string message)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            service.ShowError(message);
        });
    }

    public static void ShowFatalDispatcher(this INoticeMessageService service, string message)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            service.ShowFatal(message);
        });
    }

    public static void ShowWarnDispatcher(this INoticeMessageService service, string message)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            service.ShowWarn(message);
        });
    }

    public static void ShowInfoDispatcher(this INoticeMessageService service, string message)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            service.ShowInfo(message);
        });
    }

    public static void ShowSuccessDispatcher(this INoticeMessageService service, string message)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            service.ShowSuccess(message);
        });
    }

    public static void ShowStringDemo(this INoticeMessageService service)
    {
        Func<INoticeItem, bool> action = x =>
        {
            for (int i = 0; i < 100; i++)
            {
                x.Message = $"{i}/100";
                Thread.Sleep(20);
            }
            x.Message = $"100/100";
            Thread.Sleep(1000);
            return true;
        };
        IocMessage.Notify.ShowString(action);
    }
}
