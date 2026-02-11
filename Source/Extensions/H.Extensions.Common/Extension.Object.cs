// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

namespace H.Extensions.Common;

public static class ObjectExtension
{
    /// <summary>
    /// 创建泛型集合的实例
    /// </summary>
    /// <param name="enumerable"></param>
    /// <param name="instance"></param>
    /// <returns></returns>
    public static bool TryCreateItem(this IEnumerable enumerable, out object instance)
    {
        instance = null;

        if (enumerable == null) return false;

        Type generaicType = enumerable.GetType();

        if (!generaicType.IsGenericType)
        {
            if (generaicType.BaseType?.IsGenericType == true)
                generaicType = generaicType.BaseType;
            else
            {
                return false;
            }
        }

        Type[] args = generaicType.GetGenericArguments();

        if (args.Count() != 1) return false;

        return args[0].TryCreateInstance(out instance);
    }
    /// <summary>
    /// 创建泛型集合的类型
    /// </summary>
    /// <param name="enumerable"></param>
    /// <param name="instance"></param>
    /// <returns></returns>
    public static Type GetGenericArgumentType(this IEnumerable enumerable)
    {
        if (enumerable == null) return null;

        Type generaicType = enumerable.GetType();

        if (!generaicType.IsGenericType) return null;

        Type[] args = generaicType.GetGenericArguments();

        return args?.First();
    }

    /// <summary>
    /// 创建泛型集合的实例
    /// </summary>
    /// <param name="enumerable"></param>
    /// <param name="instance"></param>
    /// <returns></returns>
    public static object Addtem(this IEnumerable enumerable)
    {
        if (enumerable is IList list)
        {
            if (list.TryCreateItem(out object item))
            {
                list.Add(item);
                return item;
            }
        }

        return null;
    }


    public static string TryConvertToString(this object obj)
    {
        return obj.TryConvertToString(out string error);
    }
    public static string TryConvertToString(this object obj, out string error)
    {
        error = null;
        if (obj == null)
            return null;
        Type type = obj.GetType();
        TypeConverterAttribute typeConvert = type.GetCustomAttribute<TypeConverterAttribute>();
        if (typeConvert != null)
        {
            Type t = Type.GetType(typeConvert.ConverterTypeName);
            ConstructorInfo constructor = t.GetConstructors().FirstOrDefault(l => l.GetParameters().Count() == 0);
            if (constructor != null)
            {
                TypeConverter instance = Activator.CreateInstance(t) as TypeConverter;
                if (obj is DispatcherObject dispatcher && dispatcher.Dispatcher != null)
                    return dispatcher.Dispatcher?.Invoke(() => instance.ConvertToString(null, System.Globalization.CultureInfo.CurrentUICulture, obj));
                return instance.ConvertToString(null, System.Globalization.CultureInfo.CurrentUICulture, obj);
            }
        }

        if (obj is IConvertible convertible)
            return Convert.ChangeType(obj, typeof(string))?.ToString();
        error = "未识别转换方法";
        return null;
    }

    /// <summary> 模型有效信息验证 </summary>
    public static bool ModelState(this object obj, out List<string> errors)
    {
        errors = new List<string>();
        if (obj == null)
            return true;
        PropertyInfo[] propertys = obj.GetType().GetProperties();
        foreach (PropertyInfo item in propertys)
        {
            List<ValidationAttribute> collection = item.GetCustomAttributes<ValidationAttribute>()?.ToList();
            if (collection.Count == 0)
                continue;
            object value = item.GetValue(obj);

            foreach (ValidationAttribute r in collection)
            {
                if (!r.IsValid(value))
                {
                    string display = item.GetCustomAttributes<DisplayAttribute>()?.FirstOrDefault()?.Name;
                    errors.Add(r.ErrorMessage ?? r.FormatErrorMessage(display ?? item.Name));
                }
            }
        }
        return errors.Count == 0;
    }

    /// <summary> 模型有效信息验证 </summary>
    public static bool ModelStateDeep(this object obj, out string error)
    {
        error = null;
        if (obj == null)
            return true;
        PropertyInfo[] propertys = obj.GetType().GetProperties();

        var type = obj.GetType();
        if (type.FullName.StartsWith("System."))
            return true;
        foreach (PropertyInfo item in propertys)
        {
            if (item.Name == "Item")
                continue;
            List<ValidationAttribute> collection = item.GetCustomAttributes<ValidationAttribute>(false)?.ToList();
            object value = item.GetValue(obj);
            if (collection.Count > 0)
            {
                foreach (ValidationAttribute r in collection)
                {
                    if (!r.IsValid(value))
                    {
                        string display = item.GetCustomAttributes<DisplayAttribute>()?.FirstOrDefault()?.Name;
                        error = r.ErrorMessage ?? r.FormatErrorMessage(display ?? item.Name);
                        return false;
                    }
                }
                continue;
            }

            if (item.PropertyType.IsPrimitive)
                continue;
            if (item.PropertyType == typeof(string))
                continue;
            if (item.PropertyType == typeof(DateTime))
                continue;
            if (item.PropertyType.IsEnum)
                continue;

            if (value is IEnumerable objects)
            {
                foreach (object current in objects)
                {
                    if (item.PropertyType.IsPrimitive)
                        continue;
                    if (item.PropertyType == typeof(string))
                        continue;
                    if (item.PropertyType == typeof(DateTime))
                        continue;
                    if (item.PropertyType.IsEnum)
                        continue;
                    bool r = current.ModelStateDeep(out error);
                    if (r == false)
                        return false;
                }
                continue;
            }

            if (value.ModelStateDeep(out error) == false)
                return false;
        }
        return true;
    }

    public static bool IsMacth(this object obj, Func<PropertyInfo, object, bool> match)
    {
        PropertyInfo[] ps = obj.GetType().GetProperties();

        foreach (PropertyInfo p in ps)
        {
            if (!p.CanRead) continue;

            if (match?.Invoke(p, obj) == true) return true;
        }

        return false;
    }

    public static bool IsMacth(this object obj, string searchText)
    {
        if (string.IsNullOrEmpty(searchText))
            return true;

        //if (obj is ISearchable searchable)
        //    return searchable.Filter(searchText);

        Func<PropertyInfo, object, bool> match = (p, o) =>
        {
            if (p.Name == "Item")
                return false;
            if (p.PropertyType.IsValueType || p.PropertyType == typeof(string))
                return p.GetValue(obj)?.ToString()?.Contains(searchText) == true;

            return false;
        };
        return obj.IsMacth(match);
    }

    public static T TryChangeType<T>(this object obj)
    {
        bool r = obj.TryChangeType(out T result);
        return result;
    }
    public static bool TryChangeType<T>(this object obj, out T result)
    {
        if (obj is T)
        {
            result = (T)obj;
            return true;
        }

        bool r = obj.TryChangeType(typeof(T), out object robject);
        result = (T)robject;
        return r;
    }

    public static bool TryChangeType(this object obj, Type rType, out object result)
    {
        result = null;
        Type type = obj.GetType();
        if (typeof(IConvertible).IsAssignableFrom(rType) && typeof(IConvertible).IsAssignableFrom(type))
        {
            if (string.IsNullOrEmpty(obj.ToString()))
                return false;
            result = Convert.ChangeType(obj, rType);
            return true;
        }

        TypeConverterAttribute tConvert = rType.GetCustomAttribute<TypeConverterAttribute>();
        if (type == typeof(string) && tConvert != null)
        {
            Type t = Type.GetType(tConvert.ConverterTypeName);
            TypeConverter typeConverter = Activator.CreateInstance(t) as TypeConverter;
            if (typeof(Freezable).IsAssignableFrom(rType))
                result = Application.Current.Dispatcher.Invoke(() =>
                {
                    return typeConverter.ConvertFromString(obj.ToString());
                });
            else
                result = typeConverter.ConvertFromString(obj.ToString());
            return true;
        }
        return false;
    }

    public static object CloneByBasicType(this object t, Predicate<PropertyInfo> predicate = null)
    {
        Predicate<PropertyInfo> match = x =>
        {
            if (predicate?.Invoke(x) == false)
                return false;
            return x.PropertyType.IsBasicType();
        };
        return t.CloneBy(match);
    }

    public static object CloneByDisplay(this object from, Predicate<PropertyInfo> predicate = null)
    {
        return from.CloneBy(x =>
        {
            if (predicate?.Invoke(x) == false)
                return false;
            var display = x.GetCustomAttribute<DisplayAttribute>();
            if (display == null)
                return false;
            var browsable = x.GetCustomAttribute<BrowsableAttribute>();
            if (browsable?.Browsable == false)
                return false;
            return true;
        });
    }

    public static object CloneBy(this object from, Predicate<PropertyInfo> predicate = null)
    {
        var n = Activator.CreateInstance(from.GetType());
        //  Do ：基础数据
        n.CopyByBasicType(from, predicate);
        //  Do ：IList数据CloneBy递归复制
        n.CopyByList(from, predicate);
        //  Do ：TypeConverter系列数据
        n.CopyByTypeConverter(from, predicate);
        return n;
    }

    public static T CloneCast<T>(this T t, Predicate<PropertyInfo> predicate = null) where T : class, new()
    {
        return t.CloneBy(predicate) as T;
    }

    public static void CopyByBasicType<T>(this T to, T from, Predicate<PropertyInfo> predicate = null, Func<PropertyInfo, PropertyInfo, bool> firstOrDefault = null)
    {
        var ps = to.GetType().GetProperties().Where(x => x.CanWrite && x.CanWrite && predicate?.Invoke(x) != false);
        foreach (PropertyInfo p in ps)
        {
            if (p.PropertyType.IsBasicType())
                p.SetValue(to, p.GetValue(from));
        }
    }

    public static void CopyByTypeConverter<T>(this T to, T from, Predicate<PropertyInfo> predicate = null)
    {
        var ps = to.GetType().GetProperties().Where(x => x.CanWrite && x.CanWrite && predicate?.Invoke(x) != false);
        foreach (PropertyInfo p in ps)
        {
            var typeConverter = p.GetTypeConverter();
            if (typeConverter == null)
                continue;
            var value = p.GetValue(from);
            if (value == null)
            {
                p.SetValue(to, value);
                continue;
            }
            var str = typeConverter.ConvertToInvariantString(value);
            var nvalue = typeConverter.ConvertFromInvariantString(str);
            if (nvalue is Freezable freezable)
                freezable.Freeze();
            p.SetValue(to, nvalue);
        }
    }

    public static void CopyByList<T>(this T to, T from, Predicate<PropertyInfo> predicate = null)
    {
        var ps = to.GetType().GetProperties().Where(x => x.CanWrite && x.CanWrite && predicate?.Invoke(x) != false);
        foreach (PropertyInfo p in ps)
        {
            if (!typeof(IList).IsAssignableFrom(p.PropertyType))
                return;
            if (p.GetValue(to) is IList list && p.GetValue(from) is IList old)
            {
                list.Clear();
                foreach (object item in old)
                {
                    list.Add(item.CloneBy(predicate));
                }
            }
        }
    }

    public static void Copy<T>(this T to, T from, Predicate<PropertyInfo> predicate = null)
    {
        to.CopyByBasicType(from, predicate);
        to.CopyByList(from, predicate);
        to.CopyByTypeConverter(from, predicate);
    }

    public static T ToByTypeConverter<T>(this string str)
    {
        var converter = TypeDescriptor.GetConverter(typeof(T));
        return (T)converter.ConvertFromInvariantString(str);
    }

    public static IEnumerable<T> ToEnumerable<T>(this T t)
    {
        yield return t;
    }

}
