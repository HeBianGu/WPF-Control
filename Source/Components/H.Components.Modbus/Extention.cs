// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace H.Components.Modbus;

public static class Extention
{
    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="service"></param>
    public static void AddModbus(this IServiceCollection services)
    {
        //services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<ISerializableModbusDataService, SerializableModbusDataService>());
        //if (setupAction != null)
        //    services.Configure(new Action<AboutOptions>(setupAction));
    }

    ///// <summary>
    ///// 配置
    ///// </summary>
    ///// <param name="service"></param>
    //public static void UseAboutOptions(this IApplicationBuilder service, Action<IAboutOptions> action = null)
    //{
    //    action?.Invoke(AboutOptions.Instance);
    //    IocSetting.Instance.Add(AboutOptions.Instance);
    //}
}
