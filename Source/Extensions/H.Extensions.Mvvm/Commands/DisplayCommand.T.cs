// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Mvvm.Commands;

namespace H.Extensions.Mvvm.Commands;

public class DisplayCommand<T> : RelayCommand<T>, IDisplayCommand
{
    public DisplayCommand(Action<T> executeCommand) : base(executeCommand)
    {

    }

    public DisplayCommand(Action<T> executeCommand, Predicate<T> canExecuteCommand) : base(executeCommand, canExecuteCommand)
    {

    }
    public string Name { get; set; }
    public string Description { get; set; }
    public string GroupName { get; set; }
    public int Order { get; set; }
    public string Icon { get; set; }
}
