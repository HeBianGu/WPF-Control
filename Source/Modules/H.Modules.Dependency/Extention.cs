// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Modules.Dependency;
using H.Services.Setting;
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
        public static void AddDependency(this IServiceCollection services, Action<IDependencyOptions> setupAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<IDependencyViewPresenter, DependencyViewPresenter>());
            if (setupAction != null)
            {
                //setupAction.Invoke(DependencyOptions.Instance);
                services.Configure(new Action<DependencyOptions>(setupAction));
            }
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static void UseDependencyOptions(this IApplicationBuilder service, Action<IDependencyOptions> action = null)
        {
            action?.Invoke(DependencyOptions.Instance);
            IocSetting.Instance.Add(DependencyOptions.Instance);
        }
    }
}
