// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Services.Message.Dialog.Commands;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Globalization;
[Icon(FontIcons.Globe)]
[Display(Name = "语言设置", Description = "显示设置语言")]
public class ShowGlobalizationViewCommand : ShowIocPresenterCommandBase<IGlobalizationViewPresenter>
{
    public ShowGlobalizationViewCommand()
    {
        this.VerticalContentAlignment = VerticalAlignment.Center;
    }
}
