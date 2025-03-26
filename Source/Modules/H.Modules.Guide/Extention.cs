// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

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
    public static IServiceCollection AddGuideService(this IServiceCollection services, Action<GuideOptions> setupAction = null)
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<IGuideService, GuideService>());
        if (setupAction != null)
            services.Configure(setupAction);
        return services;
    }

    public static void UseGuideSetting(this IApplicationBuilder service, Action<IGuideOptions> action = null)
    {
        action?.Invoke(GuideOptions.Instance);
        IocSetting.Instance.Add(GuideOptions.Instance);
    }

    ///// <summary>
    ///// 设置显示新手向导
    ///// </summary>  
    //public static IServiceCollection AddGuideViewPresenter(this IServiceCollection services, Action<IGuideViewPresenterOption> option = null)
    //{
    //    //services.AddWindowCaptionViewPresenter();
    //    services.AddSingleton<IGuideViewPresenter, GuideViewPresenter>();
    //    option?.Invoke(GuideViewPresenter.Instance);
    //    IocSetting.Instance.Add(GuideViewPresenter.Instance);
    //    //WindowCaptionViewPresenter.Instance.AddPersenter(GuideViewPresenter.Instance);
    //    return services;
    //}
}
