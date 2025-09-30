// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Services.Logger;
using System.Net.Http;
using System.Text.Json;

namespace H.Services.Serializable.Web;

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
                IocLog.Instance?.Error(ex);
                return default;
            }
        }
    }
}