// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Windows.Input;

namespace H.Services.Serializable.TextJson;

public class TextJsonOptions
{
    public static JsonSerializerOptions GetDefaltOptions()
    {
        JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
        // 是否允许 JSON 中的尾随逗号
        jsonSerializerOptions.AllowTrailingCommas = false;
        // 是否格式化输出的 JSON，使其更具可读性
        jsonSerializerOptions.WriteIndented = true;
        // 忽略默认值的属性或字段
        jsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
        // 是否包含字段（不仅仅是属性）
        jsonSerializerOptions.IncludeFields = true;
        // 设置编码器，支持所有 Unicode 范围
        jsonSerializerOptions.Encoder = JavaScriptEncoder.Create(new TextEncoderSettings(UnicodeRanges.All));
        // 以允许命名的浮点文字
        jsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
        jsonSerializerOptions.Converters.Add(new DateTimeConverter());
        jsonSerializerOptions.Converters.Add(new TypeConverterJsonConverter());//把类型按TypeConverter序列化成文本
        jsonSerializerOptions.Converters.Add(new EnumConverter());
        //忽略类型
        jsonSerializerOptions.Converters.Add(new JsonIgnoreTypeConverter<ICommand>());
        return jsonSerializerOptions;
    }
}
