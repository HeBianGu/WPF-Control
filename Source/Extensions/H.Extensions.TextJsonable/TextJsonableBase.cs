// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using System;
global using System.Text.Json;
global using System.Text.Json.Serialization;

namespace H.Extensions.TextJsonable;

public abstract class TextJsonableBase : ITextJsonable
{
    public virtual object Read(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        return this.ReadJson(ref reader, options);
    }

    public virtual void Write(Utf8JsonWriter writer, JsonSerializerOptions options)
    {
        this.Write(writer, options);
    }
}
