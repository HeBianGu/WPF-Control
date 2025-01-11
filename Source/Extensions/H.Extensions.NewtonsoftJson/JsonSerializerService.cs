using H.Services.Serializable;
using Newtonsoft.Json;
using System;
using System.IO;

namespace H.Extensions.NewtonsoftJson
{
    public class JsonSerializerService : IJsonSerializerService
    {
        public object Load(string filePath, Type type)
        {
            if (!File.Exists(filePath))
                return null;
            string txt = File.ReadAllText(filePath);
            var obj = JsonConvert.DeserializeObject(txt, GetSerializerSettings());
            return obj;
        }

        public T Load<T>(string filePath)
        {
            return (T)Load(filePath, typeof(T));
        }

        public void Save(string filePath, object sourceObj, string xmlRootName = null)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }
            var txt = JsonConvert.SerializeObject(sourceObj, GetSerializerSettings());
            File.WriteAllText(filePath, txt);
        }

        private JsonSerializerSettings GetSerializerSettings()
        {
            return NewtonsoftJsonOptions.Instance.JsonSerializerSettings;
        }
    }
}
