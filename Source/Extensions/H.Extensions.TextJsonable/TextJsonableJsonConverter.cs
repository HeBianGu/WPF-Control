namespace H.Extensions.TextJsonable;

public class TextJsonableJsonConverter : JsonConverter<ITextJsonable>
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(ITextJsonable).IsAssignableFrom(objectType);
    }

    public override ITextJsonable Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        //  ToDo：定义接口读取Type的实现，根据Type创建实例，然后调用Read方法
        var jsonable = Activator.CreateInstance(typeToConvert) as ITextJsonable;
        jsonable.Read(ref reader, options);
        return jsonable;
    }

    //public override ITextJsonable Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //{
    //   var jsonable= Activator.CreateInstance(typeToConvert) as ITextJsonable;

    //    jsonable.Read(ref reader, typeToConvert, options);
    //    //// 读取 JSON 对象
    //    //while (reader.Read())
    //    //{
    //    //    if (reader.TokenType == JsonTokenType.PropertyName)
    //    //    {
    //    //        string propertyName = reader.GetString();
    //    //        reader.Read(); // 移动到属性值

    //    //        // 使用反射设置属性值
    //    //        PropertyInfo propertyInfo = typeToConvert.GetProperty(propertyName);
    //    //        if (propertyInfo != null)
    //    //        {
    //    //            switch (reader.TokenType)
    //    //            {
    //    //                case JsonTokenType.String:
    //    //                    propertyInfo.SetValue(jsonable, reader.GetString());
    //    //                    break;
    //    //                case JsonTokenType.Number:
    //    //                    if (propertyInfo.PropertyType == typeof(int))
    //    //                    {
    //    //                        propertyInfo.SetValue(jsonable, reader.GetInt32());
    //    //                    }
    //    //                    else if (propertyInfo.PropertyType == typeof(double))
    //    //                    {
    //    //                        propertyInfo.SetValue(jsonable, reader.GetDouble());
    //    //                    }
    //    //                    break;
    //    //                case JsonTokenType.True:
    //    //                case JsonTokenType.False:
    //    //                    propertyInfo.SetValue(jsonable, reader.GetBoolean());
    //    //                    break;
    //    //                default:
    //    //                    throw new NotSupportedException($"Unsupported token type: {reader.TokenType}");
    //    //            }
    //    //        }
    //    //    }
    //    //}
    //}

    public override void Write(Utf8JsonWriter writer, ITextJsonable value, JsonSerializerOptions options)
    {
        //  ToDo：定义接口写Type的实现

        value.Write(writer, options);
    }
}


//public class ObjectJsonConverter : JsonConverter<object>
//{
//    public override bool CanConvert(Type typeToConvert)
//    {
//        if (typeToConvert.IsPrimitive)
//            return false;
//        if (typeToConvert == typeof(string))
//            return false;
//        if (typeToConvert == typeof(DateTime))
//            return false;
//        if (typeToConvert.IsValueType)
//            return false;
//        if (typeof(IEnumerable).IsAssignableFrom(typeToConvert))
//            return false;
//        return true;
//    }
//    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//    {
//        //ITextJsonable接口走接口
//        if (typeof(ITextJsonable).IsAssignableFrom(typeToConvert))
//        {
//            var jsonable = Activator.CreateInstance(typeToConvert) as ITextJsonable;
//            jsonable.Read(ref reader, options);
//            return jsonable;
//        }

//        // 读取 JSON 数据
//        using JsonDocument doc = JsonDocument.ParseValue(ref reader);
//        JsonElement root = doc.RootElement;

//        //如果是抽象类或接口，且有$type属性，根据$type属性创建实例
//        if (typeToConvert.IsAbstract || typeToConvert.IsInterface)
//        {
//            if (root.TryGetProperty("$type", out JsonElement value) && value.ValueKind != JsonValueKind.Null)
//            {
//                var typeName = value.GetString();
//                var type = Type.GetType(typeName);
//                //TypeJsonWriterJsonable typeJsonWriterJsonable = new TypeJsonWriterJsonable();
//                //使用 JsonSerializer 反序列化属性值
//                var propertyValue = JsonSerializer.Deserialize(root.GetRawText(), type, options);
//                return propertyValue;
//            }
//        }

//        {
//            var propertyValue = JsonSerializer.Deserialize(root.GetRawText(), typeToConvert, options);
//            return propertyValue;
//        }

//        //var data = Activator.CreateInstance(typeToConvert);
//        ////// 创建目标对象
//        ////var obj = Activator.CreateInstance<T>();
//        //// 填充对象
//        //PopulateObject(data, root, options);
//        //return data;
//    }

//    private void PopulateObject(object textJsonable, JsonElement element, JsonSerializerOptions options)
//    {
//        // 获取目标对象的所有属性
//        var properties = textJsonable.GetJsonPropertyInfos();

//        foreach (var property in properties)
//        {
//            if (element.TryGetProperty(property.Name, out JsonElement value))
//            {
//                //if (property.PropertyType.IsInterface && property.PropertyType.IsAbstract
//                //    && value.ValueKind != JsonValueKind.Null
//                //    && value.TryGetProperty("$type", out JsonElement typeElement))
//                //{
//                //    var typeName = typeElement.GetString();
//                //    var type = Type.GetType(typeName);
//                //    //TypeJsonWriterJsonable typeJsonWriterJsonable = new TypeJsonWriterJsonable();
//                //    // 使用 JsonSerializer 反序列化属性值
//                //    var propertyValue = JsonSerializer.Deserialize(value.GetRawText(), type, options);
//                //    property.SetValue(textJsonable, propertyValue);
//                //}
//                //else
//                //{
//                // 使用 JsonSerializer 反序列化属性值
//                var propertyValue = JsonSerializer.Deserialize(value.GetRawText(), property.PropertyType, options);
//                property.SetValue(textJsonable, propertyValue);
//                //}
//            }
//        }
//    }

//    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
//    {
//        //ITextJsonable接口走接口
//        if (value is ITextJsonable textJsonable)
//        {
//            textJsonable.Write(writer, options);
//            return;
//        }

//        if (!this.CanConvert(value.GetType()))
//        {
//            JsonSerializer.Serialize(writer, value, value.GetType(), options);
//            return;
//        }

//        if (value is IEnumerable objects)
//        {
//            JsonSerializer.Serialize(writer, objects, value.GetType(), options);
//            return;
//        }

//        value.WriteJson(writer, options);
//    }
//}
