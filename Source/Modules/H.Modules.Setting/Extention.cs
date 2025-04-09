// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Modules.Setting;
using H.Modules.Setting.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Runtime.CompilerServices;

namespace System;

public static class SystemSettingExtention
{
    public static IServiceCollection AddSetting(this IServiceCollection services, Action<ISettingViewOptions> setupAction = null)
    {
        services.AddSettingDataService(setupAction);
        services.TryAdd(ServiceDescriptor.Singleton<ISettingViewPresenter, SettingViewPresenter>());
        //services.TryAdd(ServiceDescriptor.Singleton<ISettingButtonPresenter, SettingButtonPresenter>());
        return services;
    }

    public static IServiceCollection AddSettingDataService(this IServiceCollection services, Action<ISettingViewOptions> setupAction = null)
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<ISettingDataService, SettingDataService>());
        if (setupAction != null)
            services.Configure(new Action<SettingViewOptions>(setupAction));
        return services;
    }

    public static IApplicationBuilder UseSettingDataOptions(this IApplicationBuilder builder, Action<ISettingDataOption> option = null)
    {
        option?.Invoke(IocSetting.Instance);
        return builder;
    }

    public static IApplicationBuilder UseSettingViewOptions(this IApplicationBuilder builder, Action<ISettingViewOptions> option = null)
    {
        IocSetting.Instance.Add(SettingViewOptions.Instance);
        option?.Invoke(SettingViewOptions.Instance);
        return builder;
    }

    public static IApplicationBuilder UseSettingSecurityOptions(this IApplicationBuilder builder, Action<ISettingSecurityViewOption> option = null)
    {
        IocSetting.Instance.Add(SettingSecurityViewOption.Instance);
        option?.Invoke(SettingSecurityViewOption.Instance);
        return builder;
    }

    /// <summary>
    /// 设置系统路径
    /// </summary>  
    public static IApplicationBuilder UseSettingDefaultOptions(this IApplicationBuilder builder, Action<ISettingDataOption> option = null)
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
        builder.UseSettingDataOptions(option);
        return builder;
    }
}
