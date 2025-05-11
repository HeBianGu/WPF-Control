// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Reflection;

namespace H.Extensions.Common;
public static class AssemblyExtension
{
    public static IEnumerable<T> GetInstances<T>(this Assembly assembly)
    {
        var types = assembly.GetTypes();
        types = types.Where(t => t.IsClass && !t.IsAbstract).ToArray();
        types = types.Where(t => typeof(T).IsAssignableFrom(t)).ToArray();
        return types.Select(t => Activator.CreateInstance(t)).OfType<T>();
    }

    public static IEnumerable<T> GetInstances<T>(this Type type)
    {
        return type.Assembly.GetInstances<T>();
    }
}