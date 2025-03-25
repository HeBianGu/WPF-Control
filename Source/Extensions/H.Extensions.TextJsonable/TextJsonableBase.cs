global using System;
global using System.Text.Json;
global using System.Text.Json.Serialization;

namespace H.Extensions.TextJsonable;


public abstract class TextJsonableBase : ITextJsonable
{
    public virtual object Read(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        return this.ReadJson(ref reader, options);
    }


    public virtual void Write(Utf8JsonWriter writer, JsonSerializerOptions options)
    {
        this.Write(writer, options);
    }
}
