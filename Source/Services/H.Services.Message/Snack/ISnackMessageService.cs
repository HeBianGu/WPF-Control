// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Services.Message.Snack;

public interface ISnackMessageService
{
    Task<bool?> ShowDialog(string message);
    void ShowError(string message = "运行错误");
    void ShowFatal(string message = "严重错误");
    void ShowInfo(string message = "运行完成");
    void Show(ISnackItem message);
    Task<T> ShowProgress<T>(Func<IPercentSnackItem, T> action);
    Task<T> ShowString<T>(Func<ISnackItem, T> action);
    void ShowSuccess(string message = "运行成功");
    void ShowWarn(string message = "异常警告");
}

public static class SnackMessageServiceExtension
{
    public static void ShowErrorDispatcher(this ISnackMessageService service, string message)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            service.ShowError(message);
        });
    }

    public static void ShowFatalDispatcher(this ISnackMessageService service, string message)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            service.ShowFatal(message);
        });
    }

    public static void ShowWarnDispatcher(this ISnackMessageService service, string message)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            service.ShowWarn(message);
        });
    }

    public static void ShowInfoDispatcher(this ISnackMessageService service, string message)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            service.ShowInfo(message);
        });
    }

    public static void ShowSuccessDispatcher(this ISnackMessageService service, string message)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            service.ShowSuccess(message);
        });
    }
}
