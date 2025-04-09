using H.Controls.TagBox;
using H.Services.Common;
using H.Services.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

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
