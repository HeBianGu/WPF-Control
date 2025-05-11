// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.NewtonsoftJson;
using H.Services.Serializable;
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
    public static IServiceCollection AddNewtonsoftJsonSerializerService(this IServiceCollection services, Action<INewtonsoftJsonOptions> setupAction = null)
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<IJsonSerializerService, NewtonsoftJsonSerializerService>());
        if (setupAction != null)
            services.Configure(new Action<NewtonsoftJsonOptions>(setupAction));
        return services;
    }

    [Obsolete("目前没什么用")]
    public static IApplicationBuilder UseNewtonsoftJsonOptions(this IApplicationBuilder builder, Action<INewtonsoftJsonOptions> option = null)
    {
        IocSetting.Instance.Add(NewtonsoftJsonOptions.Instance);
        option?.Invoke(NewtonsoftJsonOptions.Instance);
        return builder;
    }
}
