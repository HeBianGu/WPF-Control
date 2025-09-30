// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Command;

[Icon("\xE713")]
[Display(Name = "异步百分比", Description = "异步执行任务，更新百分比")]
public class AsyncPercentCommand : DisplayMarkupCommandBase
{
    private double _value;
    public double Value
    {
        get { return _value; }
        set
        {
            _value = value;
            RaisePropertyChanged();
        }
    }

    public override async Task ExecuteAsync(object parameter)
    {
        await Task.Run(() =>
        {
            for (int i = 0; i <= 100; i++)
            {
                this.Value = i;
                Thread.Sleep(50);
            }
        });
    }
}
