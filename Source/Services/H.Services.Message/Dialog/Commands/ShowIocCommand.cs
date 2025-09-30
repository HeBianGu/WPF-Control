// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Services.Message.Dialog.Commands;

public class ShowIocCommand : ShowMessageDialogCommandBase
{
    public Type Type { get; set; }
    public override bool CanExecute(object parameter)
    {
        return this.Type != null;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        object p = Ioc.Services.GetService(this.Type);
        await IocMessage.ShowDialog(p, x =>
         {
             x.DialogButton = DialogButton.Sumit;
             x.Title = this.Name;
             this.Invoke(x);
         });
    }
}
