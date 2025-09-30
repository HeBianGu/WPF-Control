// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using Newtonsoft.Json.Linq;
using System.Net;

namespace H.Extensions.Http;

public interface IHttpService
{
    JContainer GET(string Url, string postDataStr, ref CookieContainer cookie);
    JContainer Post(string Url, string jsonParas, out string error);
    string Post(string Url, string postDataStr, ref CookieContainer cookie);
    bool PostData(string url, IDictionary<string, string> paras, out JContainer jsonResult, out string errorInfor, bool isJson = false);
}
