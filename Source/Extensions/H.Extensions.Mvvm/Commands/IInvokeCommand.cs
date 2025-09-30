// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Mvvm.Commands;

public interface IInvokeCommand : ICommand
{
    string Name { get; set; }
    bool IsBusy { get; set; }
    bool IsEnabled { get; set; }
    bool IsIndeterminate { get; set; }
    bool IsVisible { get; set; }
    //ILogService Logger { get; }
    string Message { get; set; }
    double Percent { get; set; }
    string GroupName { get; set; }
}
