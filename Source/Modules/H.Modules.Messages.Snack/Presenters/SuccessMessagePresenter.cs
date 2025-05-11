// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Extensions.FontIcon;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Messages.Snack
{
    [Icon(FontIcons.Completed)]
    [Display(Name = "成功消息", Description = "这是一条成功消息")]
    public class SuccessMessagePresenter : MessagePresenterBase
    {
        public SuccessMessagePresenter()
        {
            this.Level = 1;
        }
    }
}
