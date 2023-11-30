// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.IO;
using System.Net.Http;
using System.Xml.Serialization;

namespace H.Providers.Ioc
{
    public interface IWebXmlSerializerService
    {
        T Load<T>(string uri, out string message);
    }

    public class XmlWebSerializer : Ioc<IWebXmlSerializerService>
    {

    }

    public class XmlWebSerializerService : IWebXmlSerializerService
    {
        public T Load<T>(string url, out string message)
        {
            message = null;
            Uri uri = new Uri(url);
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string xml = client.GetStringAsync(uri).Result;
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    System.Xml.XmlTextReader xmlTextReader = new System.Xml.XmlTextReader(new StringReader(xml)) { XmlResolver = null };
                    return (T)xmlSerializer.Deserialize(xmlTextReader);
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    IocLog.Instance?.Error(ex);
                    return default(T);
                }
            }
        }
    }
}