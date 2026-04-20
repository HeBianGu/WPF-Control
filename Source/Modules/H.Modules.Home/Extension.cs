// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Modules.Home;
using H.Modules.Home.Base;
using H.Modules.Home.Presenters;
using H.Services.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace System;
public static class Extension
{
    public static IServiceCollection AddProjectHome(this IServiceCollection services, Action<IHomeOptions> setupAction = null)
    {
        return services.AddHome<ProjectHomeViewPresenter>();
    }

    public static IServiceCollection AddHome<T>(this IServiceCollection services, Action<IHomeOptions> setupAction = null) where T : class, IHomeViewPresenter
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<IHomeViewPresenter, T>());
        if (setupAction != null)
            services.Configure(new Action<HomeOptions>(setupAction));
        return services;
    }

    public static IApplicationBuilder UseHomeOptions(this IApplicationBuilder builder, Action<IHomeOptions> option = null)
    {
        IocSetting.Instance.Add(HomeOptions.Instance);
        option?.Invoke(HomeOptions.Instance);
        return builder;
    }
}
