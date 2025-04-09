// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Modules.License;
using H.Services.Common;
using H.Services.Common.SplashScreen;
using H.Services.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace System
{
    public static class LicenseExtention
    {
        public static IServiceCollection AddLicenseService(this IServiceCollection service, Action<ILicenseOptions> setupAction = null)
        {
            service.AddOptions();
            service.TryAdd(ServiceDescriptor.Singleton<ILicenseService, LicenseService>());
            service.TryAdd(ServiceDescriptor.Singleton<ILicenseLoadService, LicenseLoadService>());
            service.TryAdd(ServiceDescriptor.Singleton<ILicenseViewPresenter, LicenseViewPresenter>());
            service.TryAdd(ServiceDescriptor.Singleton<ILicenseFlagViewPresenter, LicenseFlagViewPresenter>());
            service.TryAdd(ServiceDescriptor.Singleton<IVipFlagViewPresenter, VipFlagViewPresenter>());
            if (setupAction != null)
                service.Configure(new Action<LicenseOptions>(setupAction));
            return service;
        }
        public static void UseLicense(this IApplicationBuilder service, Action<ILicenseOptions> action = null)
        {
            action?.Invoke(LicenseOptions.Instance);
            IocSetting.Instance.Add(LicenseOptions.Instance);
        }
    }
}
