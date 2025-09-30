// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Services.Message.Snack;

public class ShowDialogSnackMessageCommand : ShowSnackMessageCommandBase
{
    public override async void Execute(object parameter)
    {
        bool? r = await Ioc<ISnackMessageService>.Instance.ShowDialog(this.Message);
        if (r == true)
            Ioc<ISnackMessageService>.Instance.ShowSuccess(this.Message);
        else
            Ioc<ISnackMessageService>.Instance.ShowError(this.Message);
    }
}
