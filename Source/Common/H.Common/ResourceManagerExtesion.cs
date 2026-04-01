// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Resources;
using System.Runtime.CompilerServices;

namespace H.Common;

public static class ResourceManagerExtesion
{
    private static Dictionary<Type, ResourceManager> _cache = new Dictionary<Type, ResourceManager>();
    public static ResourceManager GetResourceManager(this Type type)
    {
        if (_cache.ContainsKey(type))
            return _cache[type];
        return _cache[type] = new ResourceManager($"{type.Assembly.GetName().Name}.Properties.Resources", type.Assembly);
    }

    public static string GetResourceString(this Type type, string key)
    {
        return type.GetResourceManager().GetString(key);
    }

    public static string GetPropertyGroupNameResourceString(this Type type, string propertyName)
    {
        return type.GetResourceManager().GetString($"Property_{type.Name}_{propertyName}_GroupName");
    }

}
