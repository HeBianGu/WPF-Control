using H.Extensions.AppPath;
using H.Services.AppPath;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace System;

public static class Extension
{
    public static IServiceCollection AddAppPath(this IServiceCollection services)
    {
        services.TryAdd(ServiceDescriptor.Singleton<IAppPathServce, AppPathServce>());
        return services;
    }
}
