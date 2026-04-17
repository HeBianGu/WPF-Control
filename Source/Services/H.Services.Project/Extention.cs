// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.DependencyInjection.Extensions;
using H.Services.Project;

namespace System;

public static class Extention
{
    public static IServiceCollection AddProjectSplashSave(this IServiceCollection services)
    {
        return services.AddProjectSplashSave<ProjectSplashSaveService>();
    }

    public static IServiceCollection AddProjectSplashSave<T>(this IServiceCollection services) where T : class, IProjectSplashSaveService
    {
        services.TryAdd(ServiceDescriptor.Singleton<IProjectSplashSaveService, T>());
        return services;
    }
}
