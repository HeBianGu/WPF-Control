using H.Controls.ScheduleBox;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Quartz;
//using H.Providers.Ioc;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

namespace System
{
    public static class Extension
    {
        public static IServiceCollection AddSchedule(this IServiceCollection services)
        {
            //services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<IScheduleService, ScheduleService>());
            //if (setupAction != null)
            //    services.Configure(setupAction);

            services.AddQuartz(x =>
            {
               
            });
            return services;
        }

        //public static IApplicationBuilder UseTag(this IApplicationBuilder builder, Action<TagOptions> option = null)
        //{
        //    SettingDataManager.Instance.Add(TagOptions.Instance);
        //    option?.Invoke(TagOptions.Instance);
        //    return builder;
        //}

        //        services.AddQuartz(q =>
        //{
        //    // this automatically registers the Microsoft Logging
        //});
    }
}
