using System.IO;
using System.Text.Json;

namespace H.Providers.Ioc
{
    public class JsonMetaSettingService : IMetaSettingService
    {
        private string GetFilePath(string id) => Path.Combine(SystemPathSetting.Instance.Data, id + ".json");
        public T Deserilize<T>(string id) where T : IMetaSetting
        {
            if (!File.Exists(this.GetFilePath(id)))
                return default;
            var txt = File.ReadAllText(this.GetFilePath(id));
            return (T)JsonSerializer.Deserialize(txt, typeof(T));
        }

        public void Serilize(object setting, string id)
        {
            var txt = JsonSerializer.Serialize(setting);
            File.WriteAllText(this.GetFilePath(id), txt);
        }
    }
}
