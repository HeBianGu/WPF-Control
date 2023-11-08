using H.Modules.Login;
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
        public static  IServiceCollection AddLogin(this IServiceCollection services, Action<LoginOptions> setupAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<ILoginViewPresenter, LoginViewPresenter>());
            if (setupAction != null)
                services.Configure(setupAction);
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
