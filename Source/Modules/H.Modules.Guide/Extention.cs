// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Services.Common.Guide;
using H.Services.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace H.Modules.Guide;

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

    public static void UseGuideOptions(this IApplicationBuilder service, Action<IGuideOptions> action = null)
    {
        action?.Invoke(GuideOptions.Instance);
        IocSetting.Instance.Add(GuideOptions.Instance);
    }
}
