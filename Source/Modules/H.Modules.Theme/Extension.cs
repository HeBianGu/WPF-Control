using H.Modules.Theme;
using H.Providers.Ioc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class Extension
    {
        public static IServiceCollection AddTheme(this IServiceCollection services, Action<ThemeOptions> setupAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<IThemeViewPresenter, ThemeViewPresenter>());
            if (setupAction != null)
                services.Configure(setupAction);
            return services;
        }

        public static IApplicationBuilder UseTheme(this IApplicationBuilder builder, Action<ThemeOptions> option = null)
        {
            SettingDataManager.Instance.Add(ThemeOptions.Instance);
            option?.Invoke(ThemeOptions.Instance);
            return builder;
        }
    }
}
