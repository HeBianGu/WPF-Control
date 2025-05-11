// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Services.Message.Dialog.Commands;

public abstract class ShowIocPresenterCommandBase<T> : ShowDialogCommandBase
{
    public override async Task ExecuteAsync(object parameter)
    {
        await IocMessage.Dialog.Show(this.Service, x =>
        {
            x.DialogButton = DialogButton.Sumit;
            this.Invoke(x);
        });
    }

    protected virtual T Service => Ioc.GetService<T>();

    public override bool CanExecute(object parameter)
    {
        return Ioc.Exist<T>() && base.CanExecute(parameter);
    }
}
