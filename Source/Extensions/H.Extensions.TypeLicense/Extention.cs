// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.TypeLicense;
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
    public static IServiceCollection AddTypeLicense(this IServiceCollection services, Action<ITypeLicenseOptions> setupAction = null)
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<ITypeLicenseService, TypeLicenseService>());
        if (setupAction != null)
            services.Configure(new Action<TypeLicenseOptions>(setupAction));
        return services;
    }

    public static IApplicationBuilder UseTypeLicenseOptions(this IApplicationBuilder builder, Action<ITypeLicenseOptions> option = null)
    {
        IocSetting.Instance.Add(TypeLicenseOptions.Instance);
        option?.Invoke(TypeLicenseOptions.Instance);
        return builder;
    }
}
