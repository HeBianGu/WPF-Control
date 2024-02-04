// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace H.Providers.Ioc
{
    public class JsonSerializerService : IJsonSerializerService
    {
        public object Load(string filePath, Type type)
        {
            if (!File.Exists(filePath))
                return null;
            string txt = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize(txt, type, this.GetOptions());
        }

        public T Load<T>(string filePath)
        {
            return (T)Load(filePath, typeof(T));
        }

        public void Save(string filePath, object sourceObj, string xmlRootName = null)
        {
            if(!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }
            string txt = JsonSerializer.Serialize(sourceObj, this.GetOptions());
            System.Diagnostics.Debug.WriteLine(txt);
            File.WriteAllText(filePath, txt);
        }


        protected virtual JsonSerializerOptions GetOptions()
        {
            var jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.AllowTrailingCommas = false;
            jsonSerializerOptions.WriteIndented = true;
            jsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;//忽略默认的属性或字段
            jsonSerializerOptions.IncludeFields = true;
            jsonSerializerOptions.Encoder = JavaScriptEncoder.Create(new TextEncoderSettings(UnicodeRanges.All));
            return jsonSerializerOptions;
        }

    }
}