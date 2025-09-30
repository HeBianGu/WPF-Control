// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Revertible;
using H.Services.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace System;

public static class Extension
{
    public static IServiceCollection AddRevertible(this IServiceCollection services, Action<IRevertibleOptions> setupAction = null)
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<IRevertibleService, RevertibleService>());
        if (setupAction != null)
            services.Configure(new Action<RevertibleOptions>(setupAction));
        return services;
    }
    public static IApplicationBuilder UseRevertibleOptions(this IApplicationBuilder builder, Action<IRevertibleOptions> option = null)
    {
        IocSetting.Instance.Add(RevertibleOptions.Instance);
        option?.Invoke(RevertibleOptions.Instance);
        return builder;
    }
}
