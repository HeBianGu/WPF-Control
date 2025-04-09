using H.Services.AppPath;
using H.Services.Common.Serialize.Meta;
using System.IO;

namespace H.Extensions.TextJsonable;


public class TextJsonMetaSettingService : JsonMetaSettingServiceBase
{
    public override T Deserilize<T>(string id)
    {
        if (!File.Exists(this.GetFilePath(typeof(T).Name, id)))
            return default;
        string txt = File.ReadAllText(this.GetFilePath(typeof(T).Name, id));
        return (T)JsonSerializer.Deserialize(txt, typeof(T));
    }

    public override void Serilize(object setting, string id)
    {
        string txt = JsonSerializer.Serialize(setting);
        File.WriteAllText(this.GetFilePath(setting.GetType().Name, id), txt);
    }

    protected string GetFilePath(string typeName, string id)
    {
        string path = Path.Combine(AppPaths.Instance.Cache, typeName, id + ".json");
        if (!Directory.Exists(Path.GetDirectoryName(path)))
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        return path;
    }
}
