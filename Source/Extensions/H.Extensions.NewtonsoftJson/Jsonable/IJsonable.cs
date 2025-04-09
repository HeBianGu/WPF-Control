using Newtonsoft.Json;

namespace H.Extensions.NewtonsoftJson.Jsonable;

public interface IJsonable
{
    void ReadJson(JsonReader reader, JsonSerializer serializer, object existingValue);

    void WriteJson(JsonWriter writer, JsonSerializer serializer);

}

public class MyJsonable : NewtonsoftJsonableBase
{
    public int Value { get; set; } = 222;
    public override void ReadJson(JsonReader reader, JsonSerializer serializer, object existingValue)
    {
        base.ReadJson(reader, serializer, existingValue);
    }

    public override void WriteJson(JsonWriter writer, JsonSerializer serializer)
    {
        base.WriteJson(writer, serializer);
    }
}
