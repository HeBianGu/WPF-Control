// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Common.Commands;
using H.Extensions.FontIcon;
using H.Services.Message;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Sponsor;

[Icon(FontIcons.QRCode)]
[Display(Name = "赞助支持", Description = "应用此功能给予赞助支持")]
public class ShowSponsorCommand : DisplayMarkupCommandBase
{
    public override bool CanExecute(object parameter)
    {
        return Ioc.Exist<ISponsorPresenter> != null && base.CanExecute(parameter);
    }
    public override async Task ExecuteAsync(object parameter)
    {
        await IocMessage.ShowDialog(Ioc.GetService<ISponsorPresenter>());
    }
}
