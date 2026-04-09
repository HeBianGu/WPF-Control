// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Services.Message.Dialog.Commands;

[Icon("\ue890")]
[Display(Name = "显示", Description = "显示呈现页面")]
public class ShowCommand : ShowMessageDialogCommandBase
{
    public ShowCommand()
    {
        this.Width = double.NaN;
        this.Height = double.NaN;
        this.Name = Properties.Resources.Show;
    }
    public object Presnter { get; set; }
    public override bool CanExecute(object parameter)
    {
        return this.Presnter != null || parameter != null;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        await IocMessage.Dialog.Show(this.Presnter ?? parameter, x =>
           {
               x.DialogButton = this.DialogButton;
               x.Title = this.Name;
               if (this.PresenterTemplate != null)
                   x.PresenterTemplate = this.PresenterTemplate;
               this.Invoke(x);
           });
    }
}
