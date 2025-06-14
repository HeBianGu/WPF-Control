// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.ApplicationBases.Default;
using H.ApplicationBases.Modules;
using H.ApplicationBases.Themes;
using Microsoft.Extensions.DependencyInjection;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddApplicationServices(this IServiceCollection services, Action<IDefaultApplicationOptions> options = null)
        {
            DefaultApplicationOptions opt = new DefaultApplicationOptions();
            options?.Invoke(opt);
            services.AddDefaultMessages();
            services.AddDefaultModuleServices(opt.GetConfigOptions<Action<IDefaultModuleOptions>>());
            services.AddDefaultThemeServices(opt.GetConfigOptions<Action<IDefaultThemeOptions>>());
            services.AddLog4net();
        }

        public static void UseApplicationOptions(this IApplicationBuilder app, Action<IDefaultApplicationOptions> options = null)
        {
            DefaultApplicationOptions opt = new DefaultApplicationOptions();
            options?.Invoke(opt);
            app.UseStyleOptions();
            app.UseDefaultModuleOptions(opt.GetConfigOptions<Action<IDefaultModuleOptions>>());
            app.UseDefaultThemeOptions(opt.GetConfigOptions<Action<IDefaultThemeOptions>>());

            app.UseAddLog4netOptions();
        }
    }
}
