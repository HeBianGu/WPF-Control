using System.ComponentModel;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Windows.Input;
using System.Windows.Threading;

namespace H.Services.Serializable;

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
public class TextJsonSerializerService : IJsonSerializerService
{
    public object DeserializeObject(string txt, Type type)
    {
        return string.IsNullOrEmpty(txt) ? null : JsonSerializer.Deserialize(txt, type, this.GetOptions());
    }

    public string SerializeObject<T>(T t)
    {
        return JsonSerializer.Serialize(t, this.GetOptions());
    }

    protected virtual JsonSerializerOptions GetOptions()
    {
        return TextJsonOptions.GetDefaltOptions();
    }
}

internal class DateTimeConverter : JsonConverter<DateTime>
{
    private readonly string _format = "yyyy-MM-dd HH:mm:ss";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        bool r = DateTime.TryParseExact(reader.GetString(), _format, null, System.Globalization.DateTimeStyles.None, out DateTime dateTime);
        return dateTime;
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_format));
    }
}

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

public class TypeConverterJsonConverter : JsonConverter<object>
{
    private TypeConverter CreateTypeConverter(Type objectType)
    {
        TypeConverter result = TypeDescriptor.GetConverter(objectType);
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
        // 检查是否存在能够转换到字符串和从字符串转换回来的 TypeConverter
        TypeConverter converter = CreateTypeConverter(objectType);
        return converter != null && converter.CanConvertFrom(typeof(string)) && converter.CanConvertTo(typeof(string));
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
            {
                action.Invoke();
            }
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