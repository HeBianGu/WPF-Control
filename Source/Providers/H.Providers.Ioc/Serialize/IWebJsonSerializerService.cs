// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.IO;
using System.Net.Http;
using System.Xml.Serialization;
using System;
using System.Text.Json;

namespace H.Providers.Ioc
{
    public interface IWebJsonSerializerService
    {
        T Load<T>(string uri, out string message);
    }

    internal class JsonWebSerializerService : IWebJsonSerializerService
    {
        public T Load<T>(string url, out string message)
        {
            message = null;
            Uri uri = new Uri(url);
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string json = client.GetStringAsync(uri).Result;
                    return (T)JsonSerializer.Deserialize(json, typeof(T));
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    Logger.Instance?.Error(ex);
                    return default(T);
                }
            }
        }
    }
}