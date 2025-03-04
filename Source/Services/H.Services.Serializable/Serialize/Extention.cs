// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Services.Serializable;
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
        public static IServiceCollection AddTextJsonSerializerService(this IServiceCollection services)
        {
            services.TryAdd(ServiceDescriptor.Singleton<IJsonSerializerService, TextJsonSerializerService>());
            return services;
        }
    }
}