﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using H.Modules.Login;
using H.Modules.Project;
using H.Services.Common;
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
        public static IServiceCollection AddProject(this IServiceCollection services, Action<ProjectOptions> setupAction = null)
        {
            return services.AddProject<ProjectService>(setupAction);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static IServiceCollection AddProject<T>(this IServiceCollection services, Action<ProjectOptions> setupAction = null) where T : class, IProjectService
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<IProjectService, T>());
            services.TryAdd(ServiceDescriptor.Singleton<IProjectViewPresenter, ProjectViewPresenter>());
            //services.TryAdd(ServiceDescriptor.Singleton<ISplashLoad, ProjectLoadService>());

            if (setupAction != null)
                services.Configure(setupAction);
            return services;
        }


        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        public static IApplicationBuilder UseProject(this IApplicationBuilder builder, Action<ProjectOptions> option = null)
        {
            SettingDataManager.Instance.Add(ProjectOptions.Instance);
            option?.Invoke(ProjectOptions.Instance);
            return builder;
        }
    }
}
