// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Services.Logger;
using System.IO;

namespace H.Services.Serializable;

public interface ISerializerService
{
    string SerializeObject<T>(T t);

    object DeserializeObject(string txt, Type type);
}

public static class SerializerServiceExtensions
{
    public static object Load(this ISerializerService service, string filePath, Type type)
    {
        if (!File.Exists(filePath))
            return null;
        System.Diagnostics.Debug.WriteLine(filePath);
        string txt = File.ReadAllText(filePath);

#if DEBUG
        try
        {
            return service.DeserializeObject(txt, type);
        }
        catch (Exception ex)
        {
            File.Delete(filePath);
            IocLog.Instance?.Error(ex);
            System.Diagnostics.Debug.WriteLine(ex);
            return null;
        }
#endif
        return service.DeserializeObject(txt, type);
    }
    public static T Load<T>(this ISerializerService service, string filePath)
    {
        return (T)service.Load(filePath, typeof(T));
    }
    public static void Save(this ISerializerService service, string filePath, object sourceObj, string xmlRootName = null)
    {
        if (!Directory.Exists(Path.GetDirectoryName(filePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        }
        string txt = service.SerializeObject(sourceObj);
        File.WriteAllText(filePath, txt);
    }

    public static T DeserializeObject<T>(this ISerializerService service, string txt)
    {
        return (T)service.DeserializeObject(txt, typeof(T));
    }

    public static T Clone<T>(this ISerializerService service, T t)
    {
        string txt = service.SerializeObject(t);
        return service.DeserializeObject<T>(txt);
    }
}