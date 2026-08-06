// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.IO;

namespace H.Services.Serializable.TextJson;

public static class TextJsonSerializerServiceExtention
{
    public static T DeserializeObjectByTextJson<T>(this string txt)
    {
        TextJsonSerializerService service = new TextJsonSerializerService();
        return (T)service.DeserializeObject(txt, typeof(T));
    }
    public static string SerializeObjectByTextJson(this object t)
    {
        TextJsonSerializerService service = new TextJsonSerializerService();
        return service.SerializeObject(t);
    }
    public static void SaveFileByTextJson(this object t, string file)
    {
        TextJsonSerializerService service = new TextJsonSerializerService();
        File.WriteAllText(file, service.SerializeObject(t));
    }

    public static T LoadFileByTextJsonFile<T>(this string file)
    {
        TextJsonSerializerService service = new TextJsonSerializerService();
        return (T)service.DeserializeObject(File.ReadAllText(file), typeof(T));
    }
}