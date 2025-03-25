using Path = System.IO.Path;

namespace H.Services.Common.Serialize.Meta;

public abstract class JsonMetaSettingServiceBase : IMetaSettingService
{
    public abstract T Deserilize<T>(string id);
    public abstract void Serilize(object setting, string id);
    protected string GetFilePath(string typeName, string id)
    {
        string path = Path.Combine(IocAppPaths.Instance.Cache, typeName, id + ".json");
        if (!Directory.Exists(Path.GetDirectoryName(path)))
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        return path;
    }
}
