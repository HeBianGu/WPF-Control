// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Modules.Plugin.Base;
using H.Modules.Plugin.Presenter;
using H.Services.Setting;
using Microsoft.Extensions.DependencyInjection;

namespace H.Modules.Plugin;
public static class Extension
{
    public static IServiceCollection AddPlugin(this IServiceCollection services, Action<IPluginOptions> setupAction = null)
    {
        services.AddOptions();
        services.Add(ServiceDescriptor.Singleton<IPluginManagerPresenter, PluginManagerPresenter>());
        if (setupAction != null)
            services.Configure(new Action<PluginOptions>(setupAction));
        //  Do ：注册插件子项服务
        var pluginInstances = PluginManager.GetPluginInstances<IPluginService>();
        foreach (var pluginInstance in pluginInstances)
            pluginInstance.AddPluginService(services);
        return services;
    }

    public static IApplicationBuilder UsePluginOptions(this IApplicationBuilder builder)
    {
        IocSetting.Instance.Add(PluginOptions.Instance);
        //  Do ：配置插件子项配置
        var pluginInstances = PluginManager.GetPluginInstances<IPluginService>();
        foreach (var pluginInstance in pluginInstances)
            pluginInstance.UsePluginOptions(builder);
        return builder;
    }
}
