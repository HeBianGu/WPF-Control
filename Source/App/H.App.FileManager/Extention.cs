// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

namespace H.App.FileManager
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static IServiceCollection AddAppService(this IServiceCollection services)
        {
            services.TryAdd(ServiceDescriptor.Singleton<IFileToEntityService, FileToEntityService>());
            services.TryAdd(ServiceDescriptor.Singleton<IFileToViewService, FileToViewService>());
            services.TryAdd(ServiceDescriptor.Singleton<IFileToMoreViewService, FileToMoreViewService>());
            return services;
        }
    }
}
