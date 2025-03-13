using H.Modules.SplashScreen;
using H.Services.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

namespace System
{
    public static class Extension
    {
        public static IServiceCollection AddSplashScreen(this IServiceCollection services, Action<SplashScreenOption> setupAction = null)
        {
            return services.AddSplashScreen<SplashScreenViewPresenter>();
        }
        public static IServiceCollection AddSplashScreen<T>(this IServiceCollection services, Action<SplashScreenOption> setupAction = null) where T : ISplashScreenViewPresenter
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<ISplashScreenViewPresenter, SplashScreenViewPresenter>());
            if (setupAction != null)
                services.Configure(setupAction);
            return services;
        }

        public static IApplicationBuilder UseSplashScreen(this IApplicationBuilder builder, Action<SplashScreenOption> option = null)
        {
            SettingDataManager.Instance.Add(SplashScreenOption.Instance);
            option?.Invoke(SplashScreenOption.Instance);
            return builder;
        }
    }
}
