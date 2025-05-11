// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Extensions.StoryBoard;

public static class StoryboardSetting
{
    [DefaultValue(20)]
    [Range(0, 60)]
    public static int DesiredFrameRate { get; set; } = 25;
}
