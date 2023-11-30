using H.Modules.Theme;
using H.Providers.Ioc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

namespace System
{
    public static class Extension
    {
        public static IApplicationBuilder UseTheme(this IApplicationBuilder builder, Action<ThemeSetting> option = null)
        {
            SettingDataManager.Instance.Add(ThemeSetting.Instance);
            option?.Invoke(ThemeSetting.Instance);
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
