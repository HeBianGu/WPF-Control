using H.Controls.FavoriteBox;
using H.Services.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

namespace System
{
    public static class Extension
    {
        public static IServiceCollection AddFavorite(this IServiceCollection services, Action<IFavoriteOptions> setupAction = null)
        {
            return services.AddFavorite<FavoriteService>(setupAction);
        }

        public static IServiceCollection AddFavorite<T>(this IServiceCollection services, Action<IFavoriteOptions> setupAction = null) where T : class, IFavoriteService
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<IFavoriteService, T>());
            if (setupAction != null)
                services.Configure(new Action<FavoriteOptions>(setupAction));
            return services;
        }

        public static IApplicationBuilder UseFavoriteOptions(this IApplicationBuilder builder, Action<IFavoriteOptions> option = null)
        {
            IocSetting.Instance.Add(FavoriteOptions.Instance);
            option?.Invoke(FavoriteOptions.Instance);
            return builder;
        }
    }
}
