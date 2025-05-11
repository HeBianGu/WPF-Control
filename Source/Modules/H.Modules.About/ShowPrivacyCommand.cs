// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Services.Common.About;
using H.Services.Message.Dialog.Commands;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace H.Modules.About;

[Icon("\xE946")]
[Display(Name = "隐私政策", Description = "查看系统隐私政策")]
public class ShowPrivacyCommand : IocCommandBase<IAboutViewPresenter>
{
    public override Task ExecuteAsync(object parameter)
    {
        Process.Start(new ProcessStartInfo(AboutOptions.Instance.Privacy) { UseShellExecute = true });
        return Task.CompletedTask;
    }
}

