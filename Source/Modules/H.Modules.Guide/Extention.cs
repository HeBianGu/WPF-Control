// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Modules.Guide;
using H.Services.Common.Guide;
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
    public static IServiceCollection AddGuide(this IServiceCollection services, Action<IGuideOptions> setupAction = null)
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<IGuideService, GuideService>());
        if (setupAction != null)
            services.Configure(new Action<GuideOptions>(setupAction));
        return services;
    }

    public static void UseGuide(this IApplicationBuilder service, Action<IGuideOptions> action = null)
    {
        action?.Invoke(GuideOptions.Instance);
        IocSetting.Instance.Add(GuideOptions.Instance);
    }
}
