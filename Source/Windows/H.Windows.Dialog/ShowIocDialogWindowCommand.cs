// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Interfaces;

namespace H.Windows.Dialog;

public class ShowIocDialogWindowCommand : DialogCommandBase
{
    public Type Type { get; set; }

    public override void Execute(object parameter)
    {
        var value = Ioc.Services.GetService(this.Type);
        if (value == null)
            throw new ArgumentNullException(this.Type.FullName);
        var title = value is ITitleable titleable ? titleable.Title : value.GetType().GetCustomAttribute<DisplayAttribute>()?.Name;
        DialogWindow.ShowPresenter(value, x =>
        {
            x.Width = this.Width;
            x.Height = this.Height;
            x.Title = title;
        });
    }

    public override bool CanExecute(object? parameter)
    {
        return this.Type != null;
    }
}
