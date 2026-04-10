// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Modules.Globalization;
using H.Services.Common;
using H.Services.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace System;
public static class Extension
{

    public static IServiceCollection AddGlobalization(this IServiceCollection services, Action<IGlobalizationOptions> setupAction = null)
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<ILoadGlobalizationOptionsService, LoadGlobalizationOptionsService>());
        services.TryAdd(ServiceDescriptor.Singleton<IGlobalizationViewPresenter, GlobalizationViewPresenter>());
        if (setupAction != null)
            services.Configure(new Action<GlobalizationOptions>(setupAction));
        return services;
    }

    public static IApplicationBuilder UseGlobalizations(this IApplicationBuilder builder, Action<IGlobalizationOptions> option = null)
    {
        IocSetting.Instance.Add(GlobalizationOptions.Instance);
        option?.Invoke(GlobalizationOptions.Instance);
        return builder;
    }
}
