using H.Controls.FavoriteBox;
using H.Providers.Ioc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

namespace System
{
    public static class Extension
    {
        public static IServiceCollection AddFavorite(this IServiceCollection services, Action<FavoriteOptions> setupAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<IFavoriteService, FavoriteService>());
            if (setupAction != null)
                services.Configure(setupAction);
            return services;
        }

        public static IApplicationBuilder UseFavorite(this IApplicationBuilder builder, Action<FavoriteOptions> option = null)
        {
            SettingDataManager.Instance.Add(FavoriteOptions.Instance);
            option?.Invoke(FavoriteOptions.Instance);
            return builder;
        }
    }
}
