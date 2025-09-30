// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Modules.Style;
using H.Services.Setting;

namespace System;

public static partial class Extension
{
    public static IApplicationBuilder UseStyleOptions(this IApplicationBuilder builder, Action<IStyleOptions> option = null)
    {
        option?.Invoke(StyleOptions.Instance);
        IocSetting.Instance.Add(StyleOptions.Instance);
        return builder;
    }
}
