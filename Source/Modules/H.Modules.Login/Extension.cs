// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Modules.Login.Presenters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace H.Modules.Login;

public static class Extension
{
    public static IServiceCollection AddLoginViewPresenter(this IServiceCollection services, Action<ILoginOptions> setupAction = null)
    {
        return services.AddLoginViewPresenter<LoginViewPresenter>();
    }

    public static IServiceCollection AddLoginViewPresenter<T>(this IServiceCollection services, Action<ILoginOptions> setupAction = null) where T : LoginViewPresenter
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<ILoginViewPresenter, T>());
        services.TryAdd(ServiceDescriptor.Singleton<ILoginedSplashViewPresenter, LoginedSplashViewPresenter>());
        services.AddLoginButtonViewPresenter();
        if (setupAction != null)
            services.Configure(new Action<LoginOptions>(setupAction));
        return services;
    }

    public static IServiceCollection AddRegisterLoginViewPresenter(this IServiceCollection services, Action<ILoginOptions> setupAction = null, Action<IRegistorOptions> setupRegisterAction = null)
    {
        return services.AddRegisterLoginViewPresenter<RigisterLoginViewPresenter>();
    }

    public static IServiceCollection AddRegisterLoginViewPresenter<T>(this IServiceCollection services, Action<ILoginOptions> setupAction = null, Action<IRegistorOptions> setupRegisterAction = null) where T : RigisterLoginViewPresenter
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<ILoginViewPresenter, RigisterLoginViewPresenter>());
        services.TryAdd(ServiceDescriptor.Singleton<ILoginedSplashViewPresenter, LoginedSplashViewPresenter>());
        services.AddLoginButtonViewPresenter();
        if (setupAction != null)
            services.Configure(new Action<LoginOptions>(setupAction));
        if (setupRegisterAction != null)
            services.Configure(new Action<RegistorOptions>(setupRegisterAction));
        return services;
    }


    private static IServiceCollection AddLoginButtonViewPresenter(this IServiceCollection services)
    {
        services.TryAdd(ServiceDescriptor.Singleton<ILoginButtonViewPresenter, LoginButtonViewPresenter>());
        services.TryAdd(ServiceDescriptor.Singleton<ICurrentUserViewPresenter, CurrentUserViewPresenter>());
        return services;
    }

    public static IServiceCollection AddTestLoginService(this IServiceCollection services, Action<ILoginOptions> setupAction = null)
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<ILoginService, LoginService>());
        if (setupAction != null)
            services.Configure(new Action<LoginOptions>(setupAction));
        return services;
    }

    public static IServiceCollection AddTestRegistorService(this IServiceCollection services, Action<IRegistorOptions> setupAction = null)
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<IRegisterService, RegisterService>());
        if (setupAction != null)
            services.Configure(new Action<RegistorOptions>(setupAction));
        return services;
    }

    public static IApplicationBuilder UseLoginOptions(this IApplicationBuilder builder, Action<ILoginOptions> option = null)
    {
        IocSetting.Instance.Add(LoginOptions.Instance);
        option?.Invoke(LoginOptions.Instance);
        return builder;
    }

    public static IApplicationBuilder UseRegistorOptions(this IApplicationBuilder builder, Action<IRegistorOptions> option = null)
    {
        IocSetting.Instance.Add(RegistorOptions.Instance);
        option?.Invoke(RegistorOptions.Instance);
        return builder;
    }
}
