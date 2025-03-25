using Newtonsoft.Json;

namespace H.Extensions.NewtonsoftJson.Jsonable;

public class JsonableJsonConverter : JsonConverter<IJsonable>
{
    public override IJsonable ReadJson(JsonReader reader, Type objectType, IJsonable existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;
        var jsonable = Activator.CreateInstance(objectType) as IJsonable;
        jsonable.ReadJson(reader, serializer, existingValue);
        return jsonable;
    }

    public override void WriteJson(JsonWriter writer, IJsonable value, JsonSerializer serializer)
    {
        value.WriteJson(writer, serializer);
    }
}
