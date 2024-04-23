// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Modules.Setting;
using H.Providers.Ioc;
using Microsoft.Extensions.DependencyInjection;

namespace System
{

    public static class SystemSettingExtention
    {

        /// <summary>
        /// 设置系统路径
        /// </summary>  
        public static IServiceCollection AddSetting(this IServiceCollection services)
        {
            services.AddSingleton<ISettingViewPresenter, SettingViewPresenter>();
            services.AddSingleton<ISettingButtonPresenter, SettingButtonPresenter>();
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

        /// <summary>
        /// 设置系统路径
        /// </summary>  
        public static IApplicationBuilder UseSetting(this IApplicationBuilder builder, Action<ISettingViewPresenterOption> option = null)
        {
            option?.Invoke(SettingViewPresenter.Instance);
            return builder;
        }

        /// <summary>
        /// 设置系统路径
        /// </summary>  
        public static IApplicationBuilder UseSettingDefault(this IApplicationBuilder builder, Action<ISettingDataManagerOption> option = null)
        {
            option?.Invoke(SettingDataManager.Instance);
            SettingDataManager.Instance.Settings.Add(LoginSetting.Instance);
            SettingDataManager.Instance.Settings.Add(ViewSetting.Instance);
            SettingDataManager.Instance.Settings.Add(MainSetting.Instance);
            SettingDataManager.Instance.Settings.Add(HotKeySetting.Instance);
            //SystemDisplay.Instance.Settings.Add(UpgradeSetting.Instance);
            SettingDataManager.Instance.Settings.Add(StateSetting.Instance);
            SettingDataManager.Instance.Settings.Add(FileSetting.Instance);
            SettingDataManager.Instance.Settings.Add(NotifySetting.Instance);
            SettingDataManager.Instance.Settings.Add(PasswordSetting.Instance);
            SettingDataManager.Instance.Settings.Add(MessageSetting.Instance);
            SettingDataManager.Instance.Settings.Add(PersonalSetting.Instance);
            return builder;
        }
    }

}
