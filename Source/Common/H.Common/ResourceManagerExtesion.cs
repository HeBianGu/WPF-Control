// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace H.Common;

public static class ResourceManagerExtesion
{
    const string resxFormat = "  <data name=\"{0}\" xml:space=\"preserve\">\r\n    <value>{1}</value>\r\n  </data>";
    private static Dictionary<Assembly, ResourceManager> _cache = new Dictionary<Assembly, ResourceManager>();

    public static ResourceManager GetResourceManager(this Assembly assembly)
    {
        assembly.TryGetResourceManager(out ResourceManager manager);
        return manager;
    }

    public static bool TryGetResourceManager(this Assembly assembly, out ResourceManager manager)
    {
        if (_cache.TryGetValue(assembly, out manager))
            return manager != null;

        // 约定：{AssemblyName}.Properties.Resources
        string baseName = $"{assembly.GetName().Name}.Properties.Resources";

        // 探测：清单里是否存在对应的 .resources（不抛 MissingManifestResourceException）
        string manifestName = baseName + ".resources";
        bool exists = assembly.GetManifestResourceNames().Any(x => string.Equals(x, manifestName, StringComparison.Ordinal));

        if (!exists)
        {
            manager = null;
            return false;
        }

        manager = new ResourceManager(baseName, assembly);
        _cache[assembly] = manager;
        return true;
    }

    public static string GetResx(this Assembly assembly, string key, string def = null)
    {
        string result = assembly.GetResourceManager()?.GetString(key);

#if DEBUG
        if (result == null && def != null)
            WhiteLine(key, def);
#endif
        return result;
    }

    public static string GetResx(this Type type, string key, string def = null)
    {
        var entryResult = Assembly.GetEntryAssembly().GetResourceManager()?.GetString(key);
        if (entryResult != null)
            return entryResult;
        return type.Assembly.GetResx(key, def);
    }

    public static string GetPropertyNameResx(this Type type, string propertyName, string def = null)
    {
        for (Type ctype = type; ctype != null; ctype = ctype.BaseType)
        {
            string key = $"Property_{ctype.Name}_{propertyName}";
            string result = ctype.GetResx(key, ctype == type ? def : null);
            if (result != null)
                return result;
        }
        return null;
    }

    public static string GetPropertyGroupNameResx(this Type type, string propertyName, string def = null)
    {
        for (Type ctype = type; ctype != null; ctype = ctype.BaseType)
        {
            string key = $"Property_{type.Name}_{propertyName}_GroupName";
            string result = ctype.GetResx(key, ctype == type ? def : null);
            if (result != null)
                return result;
        }
        return null;
    }

    public static string GetPropertyDescriptionResx(this Type type, string propertyName, string def = null)
    {
        for (Type ctype = type; ctype != null; ctype = ctype.BaseType)
        {
            string key = $"Property_{type.Name}_{propertyName}_Description";
            string result = ctype.GetResx(key, ctype == type ? def : null);
            if (result != null)
                return result;
        }
        return null;
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

    public static string GetStringResx(this string def, Assembly assembly, string key)
    {
        return assembly.GetResx(key, def) ?? def;
    }

    public static string GetStringResx(this string def, object assembly, string key)
    {
        return assembly.GetType().Assembly.GetResx(key, def) ?? def;
    }
}
