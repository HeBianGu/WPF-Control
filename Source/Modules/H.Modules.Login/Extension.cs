using H.Modules.Login;
using H.Providers.Ioc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

namespace System
{
    public static class Extension
    {
        public static IServiceCollection AddLoginViewPresenter(this IServiceCollection services, Action<LoginOptions> setupAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<ILoginViewPresenter, LoginViewPresenter>());
            if (setupAction != null)
                services.Configure(setupAction);
            //services.TryAdd(ServiceDescriptor.Singleton<ILoginService, LoginService>());
            return services;
        }

        public static IServiceCollection AddTestLoginService(this IServiceCollection services, Action<LoginOptions> setupAction = null)
        {
            services.TryAdd(ServiceDescriptor.Singleton<ILoginService, LoginService>());
            return services;
        }

        public static IApplicationBuilder UseLoginSetting(this IApplicationBuilder builder, Action<LoginOptions> option = null)
        {
            SettingDataManager.Instance.Add(LoginOptions.Instance);
            option?.Invoke(LoginOptions.Instance);
            return builder;
        }
    }
}
