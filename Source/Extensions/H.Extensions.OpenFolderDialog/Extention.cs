using H.Extensions.OpenFolderDialog;
using H.Services.Message.IODialog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace System
{
    public static class Extention
    {
        public static IServiceCollection AddIOFolderDialogService(this IServiceCollection services)
        {
            return services.AddIOFolderDialogService<IOFolderDialogService>();
        }

        public static IServiceCollection AddIOFolderDialogService<T>(this IServiceCollection services) where T : class, IIOFolderDialogService
        {
            services.TryAdd(ServiceDescriptor.Singleton<IIOFolderDialogService, T>());
            return services;
        }
    }
}
