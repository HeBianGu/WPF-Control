// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Modules.Logger;
using H.Services.Common.Theme;
using H.Services.Logger;
using H.Services.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace System;
public static class Extension
{

    public static IServiceCollection AddAppLog(this IServiceCollection services, Action<ILoggerOptions> setupAction = null)
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<IAppLogPresenter, AppLogPresenter>());
        services.TryAdd(ServiceDescriptor.Singleton<IAppLogService, AppLogService>());
        if (setupAction != null)
            services.Configure(new Action<LoggerOptions>(setupAction));
        return services;
    }

    public static IApplicationBuilder UseAppLogOptions(this IApplicationBuilder builder, Action<ILoggerOptions> option = null)
    {
        IocSetting.Instance.Add(LoggerOptions.Instance);
        option?.Invoke(LoggerOptions.Instance);
        return builder;
    }

}
