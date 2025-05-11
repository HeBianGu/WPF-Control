// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Extensions.FontIcon;

namespace H.Modules.Messages.Notice
{
    [Icon(FontIcons.OverwriteWordsFillKorean)]
    [Display(Name = "警告消息", Description = "这是一条警告消息")]
    public class WarnMessagePresenter : MessagePresenterBase
    {
        public WarnMessagePresenter()
        {
            this.Level = 3;
        }
    }
}
