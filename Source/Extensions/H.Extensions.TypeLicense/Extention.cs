// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Services.Common;
using H.Services.Common.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace H.Extensions.TypeLicense;

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
        IocSetting.Instance.Add(TypeLicenseOptions.Instance);
        option?.Invoke(TypeLicenseOptions.Instance);
        return builder;
    }
}
