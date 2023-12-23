// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Modules.Upgrade;
using H.Providers.Ioc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddAutoUpgrade(this IServiceCollection services, Action<UpgradeOptions> setupAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<IUpgradeService, UpdateService>());
            //services.TryAdd(ServiceDescriptor.Singleton<ISplashLoad, UpdateService>());
            services.TryAdd(ServiceDescriptor.Singleton<IWebXmlSerializerService, XmlWebSerializerService>());
            if (setupAction != null)
                services.Configure(setupAction);

        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseUpgrade(this IApplicationBuilder service, Action<UpgradeOptions> action = null)
        {
            action?.Invoke(UpgradeOptions.Instance);
            SettingDataManager.Instance.Add(UpgradeOptions.Instance);
        }
    }
}
