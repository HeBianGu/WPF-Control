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

        public static IServiceCollection AddTheme(this IServiceCollection services, Action<IThemeOptions> setupAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<ILoadThemeOptionsService, LoadThemeOptionsService>());
            services.TryAdd(ServiceDescriptor.Singleton<ISwitchThemeViewPresenter, SwitchThemeViewPresenter>());
            services.TryAdd(ServiceDescriptor.Singleton<IColorThemeViewPresenter, ColorThemeViewPresenter>());
            if (setupAction != null)
                services.Configure(new Action<ThemeOptions>(setupAction));
            return services;
        }
        [Obsolete("AddTheme")]
        public static IServiceCollection AddLoadThemeOptionsService(this IServiceCollection services, Action<IThemeOptions> setupAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<ILoadThemeOptionsService, LoadThemeOptionsService>());
            if (setupAction != null)
                services.Configure(new Action<ThemeOptions>(setupAction));
            return services;
        }

        public static IServiceCollection AddColorThemeViewPresenter(this IServiceCollection services, Action<IColorThemeOptions> setupAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<IColorThemeViewPresenter, ColorThemeViewPresenter>());
            if (setupAction != null)
                services.Configure(new Action<ThemeOptions>(setupAction));
            return services;
        }
        public static IApplicationBuilder UseThemeOptions(this IApplicationBuilder builder, Action<IThemeOptions> option = null)
        {
            IocSetting.Instance.Add(ThemeOptions.Instance);
            option?.Invoke(ThemeOptions.Instance);
            return builder;
        }

        //public static IServiceCollection AddSwitchThemeViewPresenter(this IServiceCollection services, Action<ISwitchThemeOptions> setupAction = null)
        //{
        //    services.AddOptions();
        //    services.TryAdd(ServiceDescriptor.Singleton<ISwitchThemeViewPresenter, SwitchThemeViewPresenter>());
        //    if (setupAction != null)
        //        services.Configure(new Action<SwitchThemeOptions>(setupAction));
        //    return services;
        //}

        //public static IApplicationBuilder UseSwithThemeOptions(this IApplicationBuilder builder, Action<ISwitchThemeOptions> option = null)
        //{
        //    IocSetting.Instance.Add(SwitchThemeOptions.Instance);
        //    option?.Invoke(SwitchThemeOptions.Instance);
        //    return builder;
        //}
    }
}
