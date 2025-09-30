// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Common.Transitionable;

namespace H.Windows.Main.Commands;

public class TranslationCloseWindowCommand : CloseWindowCommand
{
    public override async void Execute(object parameter)
    {
        if (parameter is Window window)
        {
            var r = await this.ShowDialogMessage(window);
            if (r != true)
                return;

            if (window is ITransitionHostable hostable)
            {
                var task = hostable.Close(window);
                await task.ContinueWith(x =>
                  {
                      SystemCommands.CloseWindow(window);
                  });
            }
        }
    }
}
