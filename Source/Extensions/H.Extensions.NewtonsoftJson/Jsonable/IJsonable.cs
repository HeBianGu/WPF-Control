// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using Newtonsoft.Json;

namespace H.Extensions.NewtonsoftJson.Jsonable;

public interface IJsonable
{
    void ReadJson(JsonReader reader, JsonSerializer serializer, object existingValue);

    void WriteJson(JsonWriter writer, JsonSerializer serializer);

}

public class MyJsonable : NewtonsoftJsonableBase
{
    public int Value { get; set; } = 222;
    public override void ReadJson(JsonReader reader, JsonSerializer serializer, object existingValue)
    {
        base.ReadJson(reader, serializer, existingValue);
    }

    public override void WriteJson(JsonWriter writer, JsonSerializer serializer)
    {
        base.WriteJson(writer, serializer);
    }
}
