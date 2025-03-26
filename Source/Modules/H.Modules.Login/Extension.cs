using H.Modules.Login;

using H.Services.Identity;
using H.Services.Setting;
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
            services.TryAdd(ServiceDescriptor.Singleton<ILoginedSplashViewPresenter, LoginedSplashViewPresenter>());
            if (setupAction != null)
                services.Configure(setupAction);
            return services;
        }

        public static IServiceCollection AddRegisterLoginViewPresenter(this IServiceCollection services, Action<LoginOptions> setupAction = null, Action<RegistorOptions> setupRegisterAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<ILoginViewPresenter, RigisterLoginViewPresenter>());
            services.TryAdd(ServiceDescriptor.Singleton<ILoginedSplashViewPresenter, LoginedSplashViewPresenter>());
            if (setupAction != null)
                services.Configure(setupAction);
            if (setupRegisterAction != null)
                services.Configure(setupRegisterAction);
            return services;
        }

        public static IServiceCollection AddTestLoginService(this IServiceCollection services, Action<LoginOptions> setupAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<ILoginService, LoginService>());
            if (setupAction != null)
                services.Configure(setupAction);
            return services;
        }

        public static IServiceCollection AddTestRegistorService(this IServiceCollection services, Action<RegistorOptions> setupAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<IRegisterService, RegisterService>());
            if (setupAction != null)
                services.Configure(setupAction);
            return services;
        }

        public static IApplicationBuilder UseLogin(this IApplicationBuilder builder, Action<LoginOptions> option = null)
        {
            IocSetting.Instance.Add(LoginOptions.Instance);
            option?.Invoke(LoginOptions.Instance);
            return builder;
        }

        public static IApplicationBuilder UseRegistor(this IApplicationBuilder builder, Action<RegistorOptions> option = null)
        {
            IocSetting.Instance.Add(RegistorOptions.Instance);
            option?.Invoke(RegistorOptions.Instance);
            return builder;
        }
    }
}
