using H.Services.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace H.Extensions.FFMpeg;

public static class Extention
{

    public static IServiceCollection AddFFMpeg(this IServiceCollection services, Action<FFMpegOption> setupAction = null)
    {
        services.AddOptions();
        services.TryAdd(ServiceDescriptor.Singleton<IFFMpegService, FFMpegService>());
        if (setupAction != null)
            services.Configure(setupAction);
        return services;
    }

    public static void UseFFMpeg(this IApplicationBuilder builder, Action<FFMpegOption> action = null)
    {
        action?.Invoke(FFMpegOption.Instance);
        IocSetting.Instance.Add(FFMpegOption.Instance);
    }
}
