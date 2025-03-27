// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


global using H.Common.Attributes;
global using System.ComponentModel.DataAnnotations;

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
