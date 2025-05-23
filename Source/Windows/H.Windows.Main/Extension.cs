// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Services.Common.MainWindow;
using H.Services.Setting;
using H.Windows.Main;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace System;

public static partial class Extension
{
    public static IServiceCollection AddMainWindowSavableService(this IServiceCollection service, Action<MainWindowOption> setupAction = null)
    {
        service.AddOptions();
        service.TryAdd(ServiceDescriptor.Singleton<IMainWindowSavableService, MainWindowSavableService>());
        if (setupAction != null)
            service.Configure(setupAction);
        return service;
    }

    public static IApplicationBuilder UseMainWindowSetting(this IApplicationBuilder builder, Action<MainWindowOption> option = null)
    {
        IocSetting.Instance.Add(MainWindowOption.Instance);
        option?.Invoke(MainWindowOption.Instance);
        return builder;
    }

    public static IApplicationBuilder UseWindowSetting(this IApplicationBuilder builder, Action<WindowSetting> option = null)
    {
        option?.Invoke(WindowSetting.Instance);
        IocSetting.Instance.Add(WindowSetting.Instance);
        return builder;
    }
}
