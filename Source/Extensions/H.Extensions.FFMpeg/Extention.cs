// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.FFMpeg;
using H.Services.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace System;

public static class Extention
{
    public static IServiceCollection AddFFMpeg(this IServiceCollection services, Action<IFFMpegOption> setupAction = null)
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<IFFMpegService, FFMpegService>());
        if (setupAction != null)
            services.Configure(new Action<FFMpegOption>(setupAction));
        return services;
    }

    public static IApplicationBuilder UseFFMpeg(this IApplicationBuilder builder, Action<IFFMpegOption> action = null)
    {
        action?.Invoke(FFMpegOption.Instance);
        IocSetting.Instance.Add(FFMpegOption.Instance);
        return builder;
    }
}
