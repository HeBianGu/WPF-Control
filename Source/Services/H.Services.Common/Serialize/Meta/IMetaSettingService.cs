namespace H.Services.Common.Serialize.Meta;

public interface IMetaSettingService
{
    void Serilize(object setting, string id);
    T Deserilize<T>(string id);
}
