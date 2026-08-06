// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Threading;

namespace H.Services.Serializable.TextJson;

public class TypeConverterJsonConverter : JsonConverter<object>
{
    private TypeConverter CreateTypeConverter(Type objectType)
    {
        TypeConverter result = TypeDescriptor.GetConverter(objectType);
        //var converterType = result.GetType();
        //if (converterType.GetCustomAttribute<IgnoreTypeConverterJsonConverterAttribute>() != null)
        //    return null;
        return result.GetType() == typeof(TypeConverter) ? null : result;
    }
    public override bool CanConvert(Type objectType)
    {
        if (objectType.IsPrimitive)
            return false;
        if (objectType.IsEnum)
            return false;
        if (objectType == typeof(string))
            return false;
        if (objectType == typeof(DateTime))
            return false;
        TypeConverter converter = CreateTypeConverter(objectType);
        if (converter == null)
            return false;
        return converter.CanConvertFrom(typeof(string)) && converter.CanConvertTo(typeof(string));
    }

    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string str = reader.GetString();
        TypeConverter converter = CreateTypeConverter(typeToConvert);
        return typeToConvert.IsAssignableTo(typeof(DispatcherObject)) && Application.Current?.Dispatcher != null
            ? Application.Current.Dispatcher.Invoke(() =>
            {
                return converter.ConvertFromInvariantString(str);
            })
            : converter.ConvertFromInvariantString(str);
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        Action action = () =>
        {
            TypeConverter converter = CreateTypeConverter(value.GetType());
            writer.WriteStringValue(converter.ConvertToInvariantString(value));
        };

        if (value is DispatcherObject dispatcherObject)
        {
            if (dispatcherObject.CheckAccess())
                action.Invoke();
            else
            {
                dispatcherObject.Dispatcher.Invoke(() =>
                {
                    action.Invoke();
                });
            }
        }
        else
        {
            action.Invoke();
        }
    }
}
