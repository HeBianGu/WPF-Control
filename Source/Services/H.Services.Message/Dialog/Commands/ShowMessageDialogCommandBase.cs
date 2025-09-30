// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Input;

namespace H.Services.Message.Dialog.Commands;

[Icon("\xE77F")]
[Display(Name = "显示消息", Description = "显示弹窗消息")]
public abstract class ShowMessageDialogCommandBase : ShowDialogCommandBase, ICommand
{
    public string Message { get; set; } = "默认消息";
}
