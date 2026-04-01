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
    [Display(Name = "默认")]
    Default = 0,
    [Display(Name = "无")]
    None,
    [Display(Name = "第一个")]
    First
}
