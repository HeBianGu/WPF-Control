namespace H.Extensions.TextJsonable;

public interface ITextJsonable : ITextJsonableReader, ITextJsonableWriter
{

}

public interface ITextJsonableReader
{
    object Read(ref Utf8JsonReader reader, JsonSerializerOptions options);
}

public interface ITextJsonableWriter
{
    void Write(Utf8JsonWriter writer, JsonSerializerOptions options);
}
