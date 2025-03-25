using H.Services.Common;
using H.Services.Common.Setting;
using H.Services.Serializable;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace H.Extensions.NewtonsoftJson;

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
        IocSetting.Instance.Add(NewtonsoftJsonOptions.Instance);
        option?.Invoke(NewtonsoftJsonOptions.Instance);
        return builder;
    }
}
