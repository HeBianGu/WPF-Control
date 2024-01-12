using H.Controls.PDF;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

namespace System
{
    public static class Extension
    {
        //public static IServiceCollection AddTag(this IServiceCollection services, Action<TagOptions> setupAction = null)
        //{
        //    services.AddOptions();
        //    services.TryAdd(ServiceDescriptor.Singleton<ITagService, TagService>());
        //    if (setupAction != null)
        //        services.Configure(setupAction);
        //    return services;
        //}

        //public static IApplicationBuilder UseTag(this IApplicationBuilder builder, Action<TagOptions> option = null)
        //{
        //    SettingDataManager.Instance.Add(TagOptions.Instance);
        //    option?.Invoke(TagOptions.Instance);
        //    return builder;
        //}
    }
}
