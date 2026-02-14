// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices.ActiveDirectory;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

namespace H.Extensions.Common;

public static class TypeExtension
{
    /// <summary>
    /// 尝试用构造函数递归创建实例
    /// </summary>
    /// <param name="type"></param>
    /// <param name="instance"></param>
    /// <returns></returns>
    public static bool TryCreateInstance(this Type type, out object instance)
    {
        if (type.IsValueType)
        {
            instance = Activator.CreateInstance(type);
            return true;
        }

        ConstructorInfo find = type.GetConstructor(new Type[] { });
        if (find != null)
        {
            instance = Activator.CreateInstance(type);
            return true;
        }

        ConstructorInfo[] constructors = type.GetConstructors();
        foreach (ConstructorInfo cconstructor in constructors)
        {
            ParameterInfo[] parameters = cconstructor.GetParameters();
            List<object> ps = new List<object>();
            foreach (ParameterInfo parameter in parameters)
            {
                if (!parameter.ParameterType.TryCreateInstance(out object pInstance))
                    break;
                ps.Add(pInstance);
            }
            if (ps.Count() != parameters.Count()) continue;
            instance = Activator.CreateInstance(type, ps.ToArray());
            return true;
        }
        instance = null;
        return false;
    }

    public static TypeConverter GetTypeConverter(this PropertyInfo propertyInfo)
    {
        var typeConvert = propertyInfo.GetCustomAttribute<TypeConverterAttribute>();
        if (typeConvert == null)
            typeConvert = propertyInfo.PropertyType.GetCustomAttribute<TypeConverterAttribute>();
        if (typeConvert == null)
            return null;
        Type t = Type.GetType(typeConvert.ConverterTypeName);
        ConstructorInfo constructor = t.GetConstructors().FirstOrDefault(l => l.GetParameters().Count() == 0);
        if (constructor != null)
            return Activator.CreateInstance(t) as TypeConverter;
        return null;
    }

    public static object TryConvertFromString(this Type type, string txt, out string error)
    {
        error = null;
        TypeConverter instance = TypeDescriptor.GetConverter(type);
        if (instance != null)
            return instance.ConvertFromInvariantString(txt);
        if (typeof(IConvertible).IsAssignableFrom(type))
            return Convert.ChangeType(txt, type);
        error = "未识别转换方法";
        return null;
    }

    public static string GetDisplayName(this Type type)
    {
        return type.GetCustomAttribute<DisplayAttribute>()?.Name ?? type.Name;
    }

    public static R GetAttributeValue<T, R>(this Type type, Func<T, R> func) where T : Attribute
    {
        var find = type.GetCustomAttribute<T>();
        if (find == null)
            return default;
        return func.Invoke(find);
    }

    public static IEnumerable<PropertyInfo> GetDisplayProperties(this Type type)
    {
        foreach (var item in type.GetProperties())
        {
            if (item.GetCustomAttribute<DisplayAttribute>() == null)
                continue;
            if (item.GetCustomAttribute<BrowsableAttribute>()?.Browsable == false)
                continue;
            yield return item;
        }
    }

    public static bool IsBasicType(this Type type)
    {
        return type.IsPrimitive
                || type.IsValueType
                || type == typeof(DateTime)
                || type == typeof(string)
                || type.IsEnum;
    }


    public static Type ToUnderlyingType(this Type type)
    {
        return Nullable.GetUnderlyingType(type) ?? type;
    }

}
