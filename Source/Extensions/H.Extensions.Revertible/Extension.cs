
using H.Extensions.Revertible;
using H.Providers.Ioc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class Extension
    {
        public static  IServiceCollection AddRevertible(this IServiceCollection services, Action<RevertibleOptions> setupAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<IRevertibleService, RevertibleService>());
            if (setupAction != null)
                services.Configure(setupAction);
            return services;
        }
        public static IApplicationBuilder UseLoginSetting(this IApplicationBuilder builder, Action<RevertibleOptions> option = null)
        {
            SettingDataManager.Instance.Add(RevertibleOptions.Instance);
            option?.Invoke(RevertibleOptions.Instance);
            return builder;
        }
    }
}
