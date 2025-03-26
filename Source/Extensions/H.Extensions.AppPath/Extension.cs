using H.Services.AppPath;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace H.Extensions.AppPath;

public static class Extension
{
    public static IServiceCollection AddAppPath(this IServiceCollection services)
    {
        services.TryAdd(ServiceDescriptor.Singleton<IAppPathServce, AppPathServce>());
        return services;
    }
}
