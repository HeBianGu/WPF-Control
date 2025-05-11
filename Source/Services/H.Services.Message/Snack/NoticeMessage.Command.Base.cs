// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Services.Message.Snack;

[Icon("\xE77F")]
[Display(Name = "显示提示", Description = "显示提示消息")]
public abstract class ShowSnackMessageCommandBase : DisplayMarkupCommandBase
{
    public string Message { get; set; } = "默认消息";

    public override void Execute(object parameter)
    {
        Ioc<ISnackMessageService>.Instance.ShowInfo(this.Message);
    }
}
