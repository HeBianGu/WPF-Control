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
    void ShowError(string message);
    void ShowFatal(string message);
    void ShowInfo(string message);
    void Show(INoticeItem message);
    Task<T> ShowProgress<T>(Func<IPercentNoticeItem, T> action);
    Task<T> ShowString<T>(Func<INoticeItem, T> action);
    void ShowSuccess(string message);
    void ShowWarn(string message);
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
}
