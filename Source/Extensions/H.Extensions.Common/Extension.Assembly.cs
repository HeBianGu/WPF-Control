// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace H.Extensions.Common;
public static class AssemblyExtension
{
    public static IEnumerable<T> GetInstances<T>(this Assembly assembly, params object[] args)
    {
        var types = assembly.GetTypes();
        types = types.Where(t => t.IsClass && !t.IsAbstract).ToArray();
        types = types.Where(t => typeof(T).IsAssignableFrom(t)).ToArray();
        return types.Select(t => Activator.CreateInstance(t, args)).OfType<T>();
    }

    public static IEnumerable<T> GetInstances<T>(this Type type, params object[] args)
    {
        return type.Assembly.GetInstances<T>(args);
    }


    public static IEnumerable<Assembly> GetAssemblies(this string dllFolderPath, SearchOption searchOption = SearchOption.AllDirectories)
    {
        var puginPath = dllFolderPath;
        var dlls = puginPath.GetFiles("*.dll", searchOption);
        List<Assembly> result = new List<Assembly>();
        foreach (var dll in dlls)
        {
            var assembly = Assembly.LoadFrom(dll);
            if (assembly == null)
                continue;
            result.Add(assembly);
        }
        return result;
    }

    public static IEnumerable<T> GetInstances<T>(this string dllFolderPath, Predicate<Assembly> predicate = null)
    {
        var where = dllFolderPath.GetAssemblies().Where(x => predicate?.Invoke(x) != false);
        foreach (var item in where)
        {
            foreach (var instance in item.GetInstances<T>())
            {
                yield return instance;
            }
        }
    }

}