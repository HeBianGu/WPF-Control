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

public class JsonWebSerializerService : IWebJsonSerializerService
{
    public async Task<(T value, string message)> LoadAsync<T>(string url, Action<HttpClient> option)
    {
        if (string.IsNullOrWhiteSpace(url))
            return (default, "地址不能未空");
        Uri uri = new Uri(url);
        using (HttpClient client = new HttpClient())
        {
            try
            {
                option?.Invoke(client);
                string json = await client.GetStringAsync(uri);
                return ((T)JsonSerializer.Deserialize(json, typeof(T)), null);
            }
            catch (Exception ex)
            {
                IocLog.Error(ex);
                return (default, ex.Message);
            }
        }
    }
}