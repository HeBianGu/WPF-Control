using H.Modules.Theme;
using H.Services.Common;
using H.Services.Common.Theme;
using H.Services.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

namespace System
{
    public static class Extension
    {
        public static IServiceCollection AddThemeLoadService(this IServiceCollection services, Action<IThemeOption> setupAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<IThemeLoadService, ThemeLoadService>());
            if (setupAction != null)
                services.Configure(setupAction);
            return services;
        }

        public static IServiceCollection AddColorThemeViewPresenter(this IServiceCollection services, Action<IThemeOption> setupAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<IColorThemeViewPresenter, ColorThemeViewPresenter>());
            if (setupAction != null)
                services.Configure(setupAction);
            return services;
        }
        public static IApplicationBuilder UseTheme(this IApplicationBuilder builder, Action<ThemeOptions> option = null)
        {
            IocSetting.Instance.Add(ThemeOptions.Instance);
            option?.Invoke(ThemeOptions.Instance);
            return builder;
        }

        public static IApplicationBuilder UseSwithTheme(this IApplicationBuilder builder, Action<SwitchThemeOptions> option = null)
        {
            IocSetting.Instance.Add(SwitchThemeOptions.Instance);
            option?.Invoke(SwitchThemeOptions.Instance);
            return builder;
        }

        public static IServiceCollection AddSwitchThemeViewPresenter(this IServiceCollection services, Action<SwitchThemeOptions> setupAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<ISwitchThemeViewPresenter, SwitchThemeViewPresenter>());
            if (setupAction != null)
                services.Configure(setupAction);
            return services;
        }
    }
}
