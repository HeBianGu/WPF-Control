// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Common.Commands;

namespace H.Extensions.Command;

public class AsyncMessageCommand : DisplayMarkupCommandBase
{
    public string Format { get; set; } = "正在加载数据第{0}/100条";
    private string _message;
    public string Message
    {
        get { return _message; }
        set
        {
            _message = value;
            RaisePropertyChanged();
        }
    }
    public override async Task ExecuteAsync(object parameter)
    {
        await Task.Run(() =>
        {
            for (int i = 0; i <= 100; i++)
            {
                this.Message = string.Format(this.Format, i);
                Thread.Sleep(100);
            }
            this.Message = "加载完成";
            Thread.Sleep(1000);
        });
    }
}
