// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using Microsoft.Extensions.DependencyInjection;
using Quartz;
//using H.Services.Common;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection.Extensions;

namespace System
{
    public static class Extension
    {
        public static IServiceCollection AddSchedule(this IServiceCollection services)
        {
            services.AddOptions();
            //services.TryAdd(ServiceDescriptor.Singleton<IScheduleService, ScheduleService>());
            //if (setupAction != null)
            //    services.Configure(setupAction);

            services.AddQuartz(x =>
            {

            });
            return services;
        }

        //public static IApplicationBuilder UseTag(this IApplicationBuilder builder, Action<TagOptions> option = null)
        //{
        //    IocSetting.Instance.Add(TagOptions.Instance);
        //    option?.Invoke(TagOptions.Instance);
        //    return builder;
        //}

        //        services.AddQuartz(q =>
        //{
        //    // this automatically registers the Microsoft Logging
        //});
    }
}
