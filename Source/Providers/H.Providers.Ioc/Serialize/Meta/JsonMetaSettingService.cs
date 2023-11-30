using System.IO;
using System.Text.Json;

namespace H.Providers.Ioc
{
    public class JsonMetaSettingService : IMetaSettingService
    {
        private string GetFilePath(string typeName, string id) => Path.Combine(SystemPathSetting.Instance.Cache, id + ".json");
        public T Deserilize<T>(string id) where T : IMetaSetting
        {
            if (!File.Exists(this.GetFilePath(typeof(T).Name, id)))
                return default;
            string txt = File.ReadAllText(this.GetFilePath(typeof(T).Name, id));
            return (T)JsonSerializer.Deserialize(txt, typeof(T));
        }

        public void Serilize(object setting, string id)
        {
            string txt = JsonSerializer.Serialize(setting);
            File.WriteAllText(this.GetFilePath(setting.GetType().Name, id), txt);
        }
    }
}
