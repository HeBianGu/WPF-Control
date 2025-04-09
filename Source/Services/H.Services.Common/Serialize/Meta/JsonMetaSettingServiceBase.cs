namespace H.Services.Common.Serialize.Meta;

public abstract class JsonMetaSettingServiceBase : IMetaSettingService
{
    public abstract T Deserilize<T>(string id);
    public abstract void Serilize(object setting, string id);
}
