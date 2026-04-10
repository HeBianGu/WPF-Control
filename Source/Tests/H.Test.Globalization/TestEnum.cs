// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common;
using H.ValueConverter;
using System.Globalization;

namespace H.Test.Globalization;

public enum TestEnum
{
    [Display(Name = "默认", GroupName = "分组", Description = "表示默认行为")]
    Default = 0,
    [Display(Name = "无", GroupName = "分组", Description = "表示无行为")]
    None,
    [Display(Name = "第一个", GroupName = "分组", Description = "表示第一个行为")]
    First
}
