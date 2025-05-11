// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.TagBox;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace System
{
    public static class Extension
    {
        public static IServiceCollection AddTag(this IServiceCollection services, Action<ITagOptions> setupAction = null)
        {
            return services.AddTag<TagService>(setupAction);
        }

        public static IServiceCollection AddTag<T>(this IServiceCollection services, Action<ITagOptions> setupAction = null) where T : class, ITagService
        {
            services.AddOptions();
            services.TryAdd(ServiceDescriptor.Singleton<ITagService, T>());
            if (setupAction != null)
                services.Configure(new Action<TagOptions>(setupAction));
            return services;
        }

        public static IApplicationBuilder UseTagOptions(this IApplicationBuilder builder, Action<ITagOptions> option = null)
        {
            IocSetting.Instance.Add(TagOptions.Instance);
            option?.Invoke(TagOptions.Instance);
            return builder;
        }
    }
}
