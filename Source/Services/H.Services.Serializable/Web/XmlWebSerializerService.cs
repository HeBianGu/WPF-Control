// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Services.Logger;
using System.IO;
using System.Net.Http;
using System.Xml.Serialization;

namespace H.Services.Serializable.Web;

public class XmlWebSerializerService : IWebXmlSerializerService
{
    public async Task<(T value, string message)> LoadAsync<T>(string url, Action<HttpClient> option)
    {
        if (string.IsNullOrWhiteSpace(url))
            return (default, "地址不能未空");
        Uri uri = new Uri(url);
        using (HttpClient client = new HttpClient())
        {
            option?.Invoke(client);
            try
            {
                string xml = await client.GetStringAsync(uri);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                System.Xml.XmlTextReader xmlTextReader = new System.Xml.XmlTextReader(new StringReader(xml)) { XmlResolver = null };
                return ((T)xmlSerializer.Deserialize(xmlTextReader), null);
            }
            catch (Exception ex)
            {
                IocLog.Error(ex);
                return (default, ex.Message);
            }
        }
    }
}