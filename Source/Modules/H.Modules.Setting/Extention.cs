// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Modules.Setting;
using H.Services.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace System
{
    public static class SystemSettingExtention
    {
        /// <summary>
        /// 设置系统路径
        /// </summary>  
        public static IServiceCollection AddSetting(this IServiceCollection services, Action<H.Services.Common.ISettingViewOption> setupAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<ISettingViewPresenter, SettingViewPresenter>());
            services.TryAdd(ServiceDescriptor.Singleton<ISettingButtonPresenter, SettingButtonPresenter>());
            if (setupAction != null)
                services.Configure(setupAction);
            return services;
        }

        ///// <summary>
        ///// 设置系统路径
        ///// </summary>  
        //public static IServiceCollection AddSettingViewPrenter(this IServiceCollection services, Action<ISettingViewPresenterOption> option = null)
        //{
        //    //services.AddWindowCaptionViewPresenter();
        //    services.AddSingleton<ISettingViewPresenter, SettingViewPresenter>();
        //    option?.Invoke(SettingViewPresenter.Instance);
        //    //WindowCaptionViewPresenter.Instance.AddPersenter(SettingViewPresenter.Instance);
        //    SystemDisplay.Instance.Settings.Add(SettingViewPresenter.Instance);
        //    return services;
        //}

        public static IApplicationBuilder UseSettingDataManager(this IApplicationBuilder builder, Action<ISettingDataManagerOption> option = null)
        {
            option?.Invoke(IocSetting.Instance);
            return builder;
        }

        public static IApplicationBuilder UseSetting(this IApplicationBuilder builder, Action<H.Services.Common.ISettingViewOption> option = null)
        {
            IocSetting.Instance.Add(SettingViewOption.Instance);
            option?.Invoke(SettingViewOption.Instance);
            return builder;
        }

        public static IApplicationBuilder UseSettingSecurity(this IApplicationBuilder builder, Action<ISettingSecurityViewOption> option = null)
        {
            IocSetting.Instance.Add(SettingSecurityViewOption.Instance);
            option?.Invoke(SettingSecurityViewOption.Instance);
            return builder;
        }

        /// <summary>
        /// 设置系统路径
        /// </summary>  
        public static IApplicationBuilder UseSettingDefault(this IApplicationBuilder builder, Action<ISettingDataManagerOption> option = null)
        {
            option?.Invoke(IocSetting.Instance);
            IocSetting.Instance.Settings.Add(LoginSetting.Instance);
            IocSetting.Instance.Settings.Add(ViewSetting.Instance);
            IocSetting.Instance.Settings.Add(MainSetting.Instance);
            IocSetting.Instance.Settings.Add(HotKeySetting.Instance);
            //SystemDisplay.Instance.Settings.Add(UpgradeSetting.Instance);
            IocSetting.Instance.Settings.Add(StateSetting.Instance);
            IocSetting.Instance.Settings.Add(FileSetting.Instance);
            IocSetting.Instance.Settings.Add(NotifySetting.Instance);
            IocSetting.Instance.Settings.Add(PasswordSetting.Instance);
            IocSetting.Instance.Settings.Add(MessageSetting.Instance);
            IocSetting.Instance.Settings.Add(PersonalSetting.Instance);
            builder.UseSettingDataManager(option);
            return builder;
        }
    }

}
