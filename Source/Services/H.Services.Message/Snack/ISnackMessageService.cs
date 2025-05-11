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
    void ShowError(string message);
    void ShowFatal(string message);
    void ShowInfo(string message);
    void Show(ISnackItem message);
    Task<T> ShowProgress<T>(Func<IPercentSnackItem, T> action);
    Task<T> ShowString<T>(Func<ISnackItem, T> action);
    void ShowSuccess(string message);
    void ShowWarn(string message);
}
