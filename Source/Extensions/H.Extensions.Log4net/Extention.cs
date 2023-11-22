using H.Extensions.Log4net;
using H.Extensions.Revertible;
using H.Providers.Ioc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;
using System.Reflection;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static IServiceCollection AddLog4net(this IServiceCollection services, Action<Log4netOptions> setupAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<ILogService, Log4netService>());
            if (setupAction != null)
                services.Configure(setupAction);
            return services;
        }

        public static IApplicationBuilder UseAddLog4netSetting(this IApplicationBuilder builder, Action<Log4netOptions> option = null)
        {
            SettingDataManager.Instance.Add(Log4netOptions.Instance);
            option?.Invoke(Log4netOptions.Instance);
            return builder;
        }
    }
}
