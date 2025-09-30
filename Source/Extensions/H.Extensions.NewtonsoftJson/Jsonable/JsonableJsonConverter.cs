// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using Newtonsoft.Json;

namespace H.Extensions.NewtonsoftJson.Jsonable;

public class JsonableJsonConverter : JsonConverter<IJsonable>
{
    public override IJsonable ReadJson(JsonReader reader, Type objectType, IJsonable existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;
        var jsonable = Activator.CreateInstance(objectType) as IJsonable;
        jsonable.ReadJson(reader, serializer, existingValue);
        return jsonable;
    }

    public override void WriteJson(JsonWriter writer, IJsonable value, JsonSerializer serializer)
    {
        value.WriteJson(writer, serializer);
    }
}
