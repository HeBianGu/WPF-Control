// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using Newtonsoft.Json;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace H.Extensions.NewtonsoftJson;

public class TypeConverterJsonConverter<T> : JsonConverter where T : TypeConverter
{
    T CreateTypeConverter()
    {
        return Activator.CreateInstance<T>();
    }
    public override bool CanConvert(Type objectType)
    {
        TypeConverter converter = CreateTypeConverter();
        return converter != null && converter.CanConvertFrom(typeof(string)) && converter.CanConvertTo(typeof(string));
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null;
        }

        TypeConverter converter = CreateTypeConverter();
        return converter.ConvertFromInvariantString((string)reader.Value);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }
        TypeConverter converter = CreateTypeConverter();
        writer.WriteValue(converter.ConvertToInvariantString(value));
    }

}

public class TypeConverterJsonConverter : JsonConverter
{
    TypeConverter CreateTypeConverter(Type objectType)
    {
        var result = TypeDescriptor.GetConverter(objectType);
        return result.GetType() == typeof(TypeConverter) ? null : result;
    }
    public override bool CanConvert(Type objectType)
    {
        if (objectType.IsPrimitive)
            return false;
        if (objectType.IsEnum)
            return false;
        //if (!objectType.IsClass)
        //    return false;
        //return true;
        TypeConverter converter = CreateTypeConverter(objectType);
        return converter != null && converter.CanConvertFrom(typeof(string)) && converter.CanConvertTo(typeof(string));
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null;
        }
        TypeConverter converter = CreateTypeConverter(objectType);
        if (reader?.Value is string str && converter != null)
            return converter.ConvertFromInvariantString(str);
        return reader.Value;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }
        TypeConverter converter = CreateTypeConverter(value.GetType());
        if (converter == null)
            writer.WriteValue(value);
        if (value is DispatcherObject dispatcherObject && dispatcherObject.Dispatcher != null)
        {
            dispatcherObject.Dispatcher.Invoke(() =>
            {
                writer.WriteValue(converter.ConvertToInvariantString(value));
            });
            return;
        }
        writer.WriteValue(converter.ConvertToInvariantString(value));
    }

}
