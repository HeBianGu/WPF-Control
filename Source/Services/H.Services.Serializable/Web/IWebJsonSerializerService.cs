// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Net.Http;

namespace H.Services.Serializable.Web;


public interface IWebSerializerService
{
    Task<(T value, string message)> LoadAsync<T>(string url, Action<HttpClient> option);

}

public interface IWebJsonSerializerService : IWebSerializerService
{
}