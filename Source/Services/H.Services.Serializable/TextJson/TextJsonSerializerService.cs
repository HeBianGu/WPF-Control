// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.ComponentModel;
using System.Text.Json;

namespace H.Services.Serializable.TextJson;
public class TextJsonSerializerService : IJsonSerializerService
{
    public virtual object DeserializeObject(string txt, Type type)
    {
        return string.IsNullOrEmpty(txt) ? null : JsonSerializer.Deserialize(txt, type, this.GetOptions());
    }

    public virtual string SerializeObject<T>(T t)
    {
        return JsonSerializer.Serialize(t, this.GetOptions());
    }

    protected virtual JsonSerializerOptions GetOptions()
    {
        return TextJsonOptions.GetDefaltOptions();
    }
}
