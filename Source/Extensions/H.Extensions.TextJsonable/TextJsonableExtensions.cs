using System.Reflection;
using System.Windows.Input;

namespace H.Extensions.TextJsonable;

public static class TextJsonableExtensions
{
    public static ITextJsonable ReadJson(this ITextJsonable jsonable, ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        // 读取 JSON 数据
        using JsonDocument doc = JsonDocument.ParseValue(ref reader);
        JsonElement root = doc.RootElement;

        //// 创建目标对象
        //var obj = Activator.CreateInstance<T>();
        // 填充对象
        jsonable.PopulateObject(root, options);
        return jsonable;
    }

    public static void WriteJson(this ITextJsonable jsonable, Utf8JsonWriter writer, JsonSerializerOptions options)
    {
        jsonable.WriteJson(writer, options, false);
    }

    public static IEnumerable<PropertyInfo> GetJsonPropertyInfos(this object value)
    {
        var result = value.GetType().GetProperties().Where(x => x.GetCustomAttribute<JsonIgnoreAttribute>() == null);
        return result.Where(x => !typeof(ICommand).IsAssignableFrom(x.PropertyType));
    }

    public static void WriteJson(this object data, Utf8JsonWriter writer, JsonSerializerOptions options, bool useType = false)
    {
        //// 使用默认的序列化逻辑
        //JsonSerializer.Serialize(writer, jsonable, options);

        // 写入类型信息
        writer.WriteStartObject();
        if (useType)
            data.WriteType(writer);

        // 写入其他属性
        foreach (var prop in data.GetJsonPropertyInfos())
        {
            writer.WritePropertyName(prop.Name);
            var value = prop.GetValue(data);
            ////如果是接口和抽象类，需要写入类型信息
            //if (prop.PropertyType.IsInterface && prop.PropertyType.IsAbstract && value != null)
            //{
            //    var typeWriter = new TypeJsonWriterJsonable(value);
            //    JsonSerializer.Serialize(writer, typeWriter, options);
            //}
            //else
            //{
            JsonSerializer.Serialize(writer, value, options);
            //}
        }
        writer.WriteEndObject();

        //// 使用默认的序列化逻辑
        //JsonSerializer.Serialize(writer, jsonable, options);
        //writer.WriteStartObject();
        //writer.WriteString("Name", value.Name);

        //// 递归写入子节点
        //writer.WritePropertyName("Children");
        //writer.WriteStartArray();
        //foreach (var child in value.Children)
        //{
        //    JsonSerializer.Serialize(writer, child, options);
        //}
        //writer.WriteEndArray();

        //writer.WriteEndObject();
    }

    public static void WriteType(this object data, Utf8JsonWriter writer)
    {
        writer.WritePropertyName("$type");
        var type = data.GetType();
        string fullName = type.FullName;
        string assemblyName = type.Assembly.GetName().Name;
        string assemblyQualifiedNameWithoutVersion = $"{fullName}, {assemblyName}";
        writer.WriteStringValue(assemblyQualifiedNameWithoutVersion);
    }

    private static void PopulateObject(this ITextJsonable textJsonable, JsonElement element, JsonSerializerOptions options)
    {
        // 获取目标对象的所有属性
        var properties = textJsonable.GetJsonPropertyInfos();

        foreach (var property in properties)
        {
            if (element.TryGetProperty(property.Name, out JsonElement value))
            {
                //if (property.PropertyType.IsInterface && property.PropertyType.IsAbstract
                //    && value.ValueKind != JsonValueKind.Null
                //    && value.TryGetProperty("$type", out JsonElement typeElement))
                //{
                //    var typeName = typeElement.GetString();
                //    var type = Type.GetType(typeName);
                //    //TypeJsonWriterJsonable typeJsonWriterJsonable = new TypeJsonWriterJsonable();
                //    // 使用 JsonSerializer 反序列化属性值
                //    var propertyValue = JsonSerializer.Deserialize(value.GetRawText(), type, options);
                //    property.SetValue(textJsonable, propertyValue);
                //}
                //else
                //{
                //使用 JsonSerializer 反序列化属性值
                var propertyValue = JsonSerializer.Deserialize(value.GetRawText(), property.PropertyType, options);
                property.SetValue(textJsonable, propertyValue);
                //}
            }
        }
    }
}

public class TypeJsonWriterJsonable : ITextJsonable
{
    private readonly object _value;
    public TypeJsonWriterJsonable(object value)
    {
        this._value = value;
    }

    public object Read(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public void Write(Utf8JsonWriter writer, JsonSerializerOptions options)
    {
        this._value.WriteJson(writer, options, true);
    }
}
