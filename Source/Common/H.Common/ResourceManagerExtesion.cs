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
    const string resxFormat = "  <data name=\"{0}\" xml:space=\"preserve\">\r\n    <value>{1}</value>\r\n  </data>";
    private static Dictionary<Type, ResourceManager> _cache = new Dictionary<Type, ResourceManager>();
    public static ResourceManager GetResourceManager(this Type type)
    {
        if (_cache.ContainsKey(type))
            return _cache[type];
        return _cache[type] = new ResourceManager($"{type.Assembly.GetName().Name}.Properties.Resources", type.Assembly);
    }

    public static string GetResx(this Type type, string key, string def = null)
    {
        var result = type.GetResourceManager().GetString(key);
#if DEBUG
        if (result == null && def != null)
            WhiteLine(key, def);
#endif
        return result;
    }
    public static string GetPropertyNameResx(this Type type, string propertyName, string def = null)
    {
        string key = $"Property_{type.Name}_{propertyName}";
        return type.GetResx(key, def);
    }

    public static string GetPropertyGroupNameResx(this Type type, string propertyName, string def = null)
    {
        string key = $"Property_{type.Name}_{propertyName}_GroupName";
        return type.GetResx(key, def);
    }

    public static string GetPropertyDescriptionResx(this Type type, string propertyName, string def = null)
    {
        string key = $"Property_{type.Name}_{propertyName}_Description";
        return type.GetResx(key, def);
    }

    public static string GetNameResx(this Type type, string def = null)
    {
        string key = $"Type_{type.Name}";
        return type.GetResx(key, def);
    }

    public static string GetGroupNameResx(this Type type, string def = null)
    {
        string key = $"Type_{type.Name}_GroupName";
        return type.GetResx(key, def);
    }

    public static string GetDescriptionResx(this Type type, string def = null)
    {
        string key = $"Type_{type.Name}_Description";
        return type.GetResx(key, def);
    }

    private static void WhiteLine(string key, string value)
    {
        string v = string.Format(resxFormat, key, value);
        System.Diagnostics.Debug.WriteLine(v);
    }

    public static string GetEnumNameResx(this Enum value, string def = null)
    {
        var type = value.GetType();
        string key = $"Enum_{type.Name}_{value.ToString()}";
        return type.GetResx(key, def);
    }

    public static string GetEnumGroupNameResx(this Enum value, string def = null)
    {
        var type = value.GetType();
        string key = $"Enum_{type.Name}_{value.ToString()}_GroupName";
        return type.GetResx(key, def);
    }
    public static string GetEnumDescriptionResx(this Enum value, string def = null)
    {
        var type = value.GetType();
        string key = $"Enum_{type.Name}_{value.ToString()}_Description";
        return type.GetResx(key, def);
    }
}
