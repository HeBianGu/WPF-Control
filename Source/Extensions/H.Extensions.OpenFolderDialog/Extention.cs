// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.OpenFolderDialog;
using H.Services.Message.IODialog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
