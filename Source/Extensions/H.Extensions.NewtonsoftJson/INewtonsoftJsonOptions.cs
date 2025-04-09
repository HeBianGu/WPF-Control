using Newtonsoft.Json;

namespace H.Extensions.NewtonsoftJson;
public interface INewtonsoftJsonOptions
{
    JsonSerializerSettings JsonSerializerSettings { get; set; }
}