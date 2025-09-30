// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Services.Operation;
using H.Services.Setting;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Operation
{
    [Icon(FontIcons.Handwriting)]
    [Display(Name = "操作日志", GroupName = SettingGroupNames.GroupAuthority, Description = "应用此功能查看操作日志")]
    public class OperationViewPresenter : IOperationViewPresenter
    {

    }
}
