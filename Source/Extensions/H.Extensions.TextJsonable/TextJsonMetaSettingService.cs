// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Services.AppPath;
using H.Services.Common.Serialize.Meta;
using System.IO;

namespace H.Extensions.TextJsonable;

public class TextJsonMetaSettingService : JsonMetaSettingServiceBase
{
    public override T Deserilize<T>(string id, string folderName = null)
    {
        string path = this.GetFilePath(folderName ?? typeof(T).Name, id);
        if (!File.Exists(path))
        {
            var defPath = this.GetDefaultTemplateFilePath(typeof(T).Name, id);
            if (!File.Exists(defPath))
                return default;
            path = defPath;
        }
        string txt = File.ReadAllText(path);
        return (T)JsonSerializer.Deserialize(txt, typeof(T));
    }

    public override void Serilize(object setting, string id, string folderName = null)
    {
        string txt = JsonSerializer.Serialize(setting);
        File.WriteAllText(this.GetFilePath(folderName ?? setting.GetType().Name, id), txt);
    }

    protected string GetFilePath(string typeName, string id)
    {
        string path = Path.Combine(AppPaths.Instance.Cache, typeName, id + ".json");
        if (!Directory.Exists(Path.GetDirectoryName(path)))
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        return path;
    }

    private string GetDefaultTemplateFilePath(string typeName, string id)
    {
        var cache = Path.GetFileNameWithoutExtension(AppPaths.Instance.Cache);
        return Path.Combine(AppDomianPaths.DefaultTemplates, cache, typeName, id + ".json");
    }
}
