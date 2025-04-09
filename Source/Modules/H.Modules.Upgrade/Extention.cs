// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Modules.Upgrade;
using H.Services.Common.Upgrade;
using H.Services.Serializable.Web;
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
    public static void AddAutoUpgrade(this IServiceCollection services, Action<IUpgradeOptions> setupAction = null)
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<IUpgradeService, UpdateService>());
        //services.TryAdd(ServiceDescriptor.Singleton<ISplashLoad, UpdateService>());
        services.TryAdd(ServiceDescriptor.Singleton<IWebXmlSerializerService, XmlWebSerializerService>());
        if (setupAction != null)
            services.Configure(new Action<UpgradeOptions>(setupAction));

    }

    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="service"></param>
    public static void UseUpgrade(this IApplicationBuilder service, Action<UpgradeOptions> action = null)
    {
        action?.Invoke(UpgradeOptions.Instance);
        IocSetting.Instance.Add(UpgradeOptions.Instance);
    }
}
