﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Extensions.TypeLicense;
using H.Services.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static IServiceCollection AddTypeLicense(this IServiceCollection services, Action<TypeLicenseOptions> setupAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<ITypeLicenseService, TypeLicenseService>());
            if (setupAction != null)
                services.Configure(setupAction);
            return services;
        }

        public static IApplicationBuilder UseTypeLicense(this IApplicationBuilder builder, Action<TypeLicenseOptions> option = null)
        {
            SettingDataManager.Instance.Add(TypeLicenseOptions.Instance);
            option?.Invoke(TypeLicenseOptions.Instance);
            return builder;
        }
    }
}
