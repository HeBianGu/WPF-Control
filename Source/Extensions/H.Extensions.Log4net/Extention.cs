// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Log4net;
using H.Services.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace System;

public static class Extention
{
    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="service"></param>
    public static IServiceCollection AddLog4net(this IServiceCollection services, Action<ILog4netOptions> setupAction = null)
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<ILogService, Log4netService>());
        if (setupAction != null)
            services.Configure(new Action<Log4netOptions>(setupAction));
        return services;
    }

    public static IApplicationBuilder UseAddLog4netOptions(this IApplicationBuilder builder, Action<ILog4netOptions> option = null)
    {
        IocSetting.Instance.Add(Log4netOptions.Instance);
        option?.Invoke(Log4netOptions.Instance);
        return builder;
    }
}
