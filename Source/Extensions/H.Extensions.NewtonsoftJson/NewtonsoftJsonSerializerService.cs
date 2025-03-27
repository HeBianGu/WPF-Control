using H.Services.Serializable;
using Newtonsoft.Json;

namespace H.Extensions.NewtonsoftJson;

public class NewtonsoftJsonSerializerService : IJsonSerializerService
{
    public object DeserializeObject(string txt, Type type)
    {
        return JsonConvert.DeserializeObject(txt, GetSerializerSettings());
    }

    public string SerializeObject<T>(T t)
    {
        return JsonConvert.SerializeObject(t, GetSerializerSettings()); ;
    }

    private JsonSerializerSettings GetSerializerSettings()
    {
        return NewtonsoftJsonOptions.Instance.JsonSerializerSettings;
    }
}

public static class NewtonsoftJsonSerializerServiceExtension
{
    public static T CloneByNewtonsoftJson<T>(this T t)
    {
        var service = new NewtonsoftJsonSerializerService();
        return service.Clone(t);
    }
}
