// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json;
using System.Windows;
using System.Windows.Threading;

namespace H.Extensions.Common;

public static class TextJsonCloneExtension
{
    private static readonly JsonSerializerOptions s_jsonCloneOptions = new JsonSerializerOptions
    {
        // 支持引用循环和共享引用
        ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
        // 同时序列化字段（视场景可改为 false）
        IncludeFields = true,
        // 在反序列化时忽略大小写匹配属性名
        PropertyNameCaseInsensitive = true,
        // 写入 null 时可选择忽略，按需调整
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never,
    };

    public static T CloneTextJson<T>(this T source, JsonSerializerOptions options = null)
    {
        if (source == null) return default!;
        var opt = options ?? s_jsonCloneOptions;
        // 序列化为字符串再反序列化
        string json = JsonSerializer.Serialize(source, opt);
        return JsonSerializer.Deserialize<T>(json, opt)!;
    }

    public static object CloneTextJson(this object source, Type targetType, JsonSerializerOptions? options = null)
    {
        if (source == null) return null;
        var opt = options ?? s_jsonCloneOptions;
        string json = JsonSerializer.Serialize(source, opt);
        return JsonSerializer.Deserialize(json, targetType, opt);
    }
}
