using H.Modules.SplashScreen;
using H.Services.Common.SplashScreen;
using H.Services.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace System;

public static class Extension
{
    public static IServiceCollection AddSplashScreen(this IServiceCollection services, Action<ISplashScreenOptions> setupAction = null)
    {
        return services.AddSplashScreen<SplashScreenViewPresenter>(setupAction);
    }
    public static IServiceCollection AddSplashScreen<T>(this IServiceCollection services, Action<ISplashScreenOptions> setupAction = null) where T : ISplashScreenViewPresenter
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<ISplashScreenViewPresenter, SplashScreenViewPresenter>());
        if (setupAction != null)
            services.Configure(new Action<SplashScreenOptions>(setupAction));
        return services;
    }

    public static IApplicationBuilder UseSplashScreenOptions(this IApplicationBuilder builder, Action<ISplashScreenOptions> option = null)
    {
        IocSetting.Instance.Add(SplashScreenOptions.Instance);
        option?.Invoke(SplashScreenOptions.Instance);
        return builder;
    }
}
