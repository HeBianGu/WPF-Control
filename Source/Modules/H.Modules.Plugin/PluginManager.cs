// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Common;
using H.Modules.Plugin.Base;
using H.Services.AppPath;
using H.Services.Logger;

namespace H.Modules.Plugin;

public static class PluginManager
{
    public static IEnumerable<T> GetPluginInstances<T>()
    {
        var where = GetPluginAssemblies();
        foreach (var item in where)
        {
            foreach (var instance in item.GetInstances<T>())
            {
                yield return instance;
            }
        }
    }
    public static IEnumerable<Assembly> GetPluginAssemblies()
    {
        List<Assembly> result = new List<Assembly>();
        foreach (var item in AppDomianPaths.Plugin.GetAssemblies())
        {
            try
            {
                if (item.GetCustomAttribute<PluginAttribute>() != null)
                    result.Add(item);
            }
            catch (Exception ex)
            {
                IocLog.Error(ex);
            }
        }
        return result;
    }
}
