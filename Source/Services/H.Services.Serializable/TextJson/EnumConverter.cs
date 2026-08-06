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

internal class EnumConverter : JsonConverter<Enum>
{
    public override Enum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string str = reader.GetString();
        return (Enum)Enum.Parse(typeToConvert, str);
    }

    public override void Write(Utf8JsonWriter writer, Enum value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}