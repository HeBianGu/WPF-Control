
using H.Extensions.Revertible;
using H.Providers.Ioc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

namespace System
{
    public static class Extension
    {
        public static IServiceCollection AddRevertible(this IServiceCollection services, Action<RevertibleOptions> setupAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<IRevertibleService, RevertibleService>());
            if (setupAction != null)
                services.Configure(setupAction);
            return services;
        }
        public static IApplicationBuilder UseLogin(this IApplicationBuilder builder, Action<RevertibleOptions> option = null)
        {
            SettingDataManager.Instance.Add(RevertibleOptions.Instance);
            option?.Invoke(RevertibleOptions.Instance);
            return builder;
        }
    }
}
