// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Services.AppPath;
using H.Services.Common.Serialize.Meta;
using Newtonsoft.Json;
using System.IO;

namespace H.Extensions.NewtonsoftJson;

public class NewtonsoftJsonMetaSettingService : JsonMetaSettingServiceBase
{
    public override T Deserilize<T>(string id)
    {
        string path = this.GetFilePath(typeof(T).Name, id);
        if (!File.Exists(path))
            return default;

        System.Diagnostics.Debug.WriteLine(path);
        string txt = File.ReadAllText(path);
        var obj = JsonConvert.DeserializeObject(txt, typeof(T), GetSerializerSettings());
        return (T)obj;
    }

    public override void Serilize(object setting, string id)
    {
        var txt = JsonConvert.SerializeObject(setting, GetSerializerSettings());
        File.WriteAllText(this.GetFilePath(setting.GetType().Name, id), txt);
    }

    private JsonSerializerSettings GetSerializerSettings()
    {
        return NewtonsoftJsonOptions.Instance.JsonSerializerSettings;
    }

    protected virtual object Deserialize(string str)
    {
        //System.Diagnostics.Debug.WriteLine(str);
        var obj = JsonConvert.DeserializeObject(str, this.GetType(), GetSerializerSettings());
        return obj;
    }
    protected string GetFilePath(string typeName, string id)
    {
        string path = Path.Combine(AppPaths.Instance.Cache, typeName, id + ".json");
        if (!Directory.Exists(Path.GetDirectoryName(path)))
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        return path;
    }
}
