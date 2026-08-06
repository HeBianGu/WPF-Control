// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Text.Json;
using System.Text.Json.Serialization;

namespace H.Services.Serializable.TextJson;

internal class JsonIgnoreTypeConverter<T> : JsonConverter<object>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(T).IsAssignableFrom(typeToConvert);
    }
    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return null;
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {

    }
}
