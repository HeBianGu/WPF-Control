using H.Extensions.NewtonsoftJson;
using H.Services.Common;
using H.Services.Serializable;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static IServiceCollection AddNewtonsoftJsonSerializerService(this IServiceCollection services, Action<NewtonsoftJsonOptions> setupAction = null)
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<IJsonSerializerService, NewtonsoftJsonSerializerService>());
            if (setupAction != null)
                services.Configure(setupAction);
            return services;
        }

        [Obsolete("目前没什么用")]
        public static IApplicationBuilder UseNewtonsoftJson(this IApplicationBuilder builder, Action<NewtonsoftJsonOptions> option = null)
        {
            SettingDataManager.Instance.Add(NewtonsoftJsonOptions.Instance);
            option?.Invoke(NewtonsoftJsonOptions.Instance);
            return builder;
        }
    }
}
