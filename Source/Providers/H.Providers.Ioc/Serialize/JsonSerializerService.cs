﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.IO;
using System.Text.Json;

namespace H.Providers.Ioc
{

    public class JsonSerializerService : ISerializerService
    {
        public object CloneXml(object o)
        {
            string txt = JsonSerializer.Serialize(o);
            return JsonSerializer.Deserialize(txt, o.GetType());
        }

        public object Load(string filePath, Type type)
        {
            if (!File.Exists(filePath))
                return null;
            string txt = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize(txt, type);
        }

        public T Load<T>(string filePath)
        {
            return (T)Load(filePath, typeof(T));
        }

        public void Save(string filePath, object sourceObj, string xmlRootName = null)
        {
            string txt = JsonSerializer.Serialize(sourceObj);
            System.Diagnostics.Debug.WriteLine(txt);
            File.WriteAllText(filePath, txt);
        }
    }
}