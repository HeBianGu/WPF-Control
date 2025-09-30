// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Services.Message.Snack;

public class ShowStringSnackMessageCommand : ShowSnackMessageCommandBase
{
    public override void Execute(object parameter)
    {
        Func<ISnackItem, bool> action = x =>
        {
            for (int i = 0; i < 100; i++)
            {
                x.Message = $"{i}/100";
                Thread.Sleep(20);
            }
            x.Message = $"100/100";
            Thread.Sleep(1000);
            return true;
        };
        Ioc<ISnackMessageService>.Instance.ShowString(action);
    }
}
