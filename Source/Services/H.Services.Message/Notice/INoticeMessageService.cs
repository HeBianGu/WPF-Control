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
