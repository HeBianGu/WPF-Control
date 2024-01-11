// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace H.Providers.Ioc
{

    public class JsonSerializerService : ISerializerService
    {

        private readonly JsonSerializerOptions _option;
        public JsonSerializerService()
        {
            var serializeOption = new JsonSerializerOptions()
            {
                AllowTrailingCommas = false,
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,//忽略默认的属性或字段
                IncludeFields = true,
            };
            _option = serializeOption;
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