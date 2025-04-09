namespace H.Extensions.TextJsonable;

public abstract class JsonBindableBase : ITextJsonable
{
    public virtual object Read(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        return this.ReadJson(ref reader, options);
    }

    public virtual void Write(Utf8JsonWriter writer, JsonSerializerOptions options)
    {
        this.WriteJson(writer, options);
    }
}
