// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Commands;
using H.Extensions.Computer.ComputerPerformanceCounters;
using H.Services.Message;
using System.ComponentModel.DataAnnotations;

namespace H.Extensions.Computer;

[Display(Name = "性能计数器", Description = "显示计算机性能计数器的命令")]
public class ShowPerformanceCountersCommand : DisplayMarkupCommandBase
{
    private IPerformanceCountersPresenter _performanceCountersPresenter;
    public ShowPerformanceCountersCommand()
    {
        _performanceCountersPresenter = CreatePerformanceCountersPresenter();
    }

    public override async Task ExecuteAsync(object parameter)
    {
        this._performanceCountersPresenter.Start();
        await IocMessage.Dialog?.Show(this._performanceCountersPresenter, x => x.Title = this.Name);
        this._performanceCountersPresenter.Stop();
    }

    protected virtual IPerformanceCountersPresenter CreatePerformanceCountersPresenter()
    {
        return new PerformanceCountersPresenter();
    }
}
