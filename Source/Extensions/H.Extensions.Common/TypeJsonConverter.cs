using System.Text.Json;
using System.Text.Json.Serialization;

namespace H.Extensions.Common;

public class TypeJsonConverter<T> : JsonConverter<T>
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string value = reader.GetString();
        return value.TryChangeType<T>();
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNull(string.Empty);
            return;
        }
        string txt = value.TryConvertToString();
        writer.WriteStringValue(txt);
    }
}
